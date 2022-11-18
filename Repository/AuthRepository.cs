#nullable disable
using Microsoft.EntityFrameworkCore;
using BookStoreManage.IRepository;
using BookStoreManage.Entity;
using System.Security.Cryptography;
using System.Text;
using BookStoreManage.DTO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin.Auth;
using Firebase.Auth;
using System.Text.RegularExpressions;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace BookStoreManage.Repository;

public class AuthRepository : IAuthRepository
{
    private Account _account;
    private readonly BookManageContext _context;
    private readonly IConfiguration _configuration;
    private readonly IAccountRepository _accountRepository;
    public AuthRepository(BookManageContext context, IConfiguration configuration, IAccountRepository accountRepository)
    {
        _context = context;
        _configuration = configuration;
        _accountRepository = accountRepository;
    }

    public async Task<Account> CheckLogin(AuthDto account)
    {
        string email = _accountRepository.Base64Encode(account.AccountEmail);
        var _acc = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.AccountEmail == email);
        if (_acc != null)
        {
            if (!VerifyPasswordHash(account.Password, _acc.PasswordHash, _acc.PasswordSalt))
            {
                throw new BadHttpRequestException("Wrong password!");
            }
        }
        else
        {
            throw new BadHttpRequestException("Email is not valid!");
        }
        if (_acc.Status == false)
        {
            throw new BadHttpRequestException("Your account have been block!");
        }
        return _acc;
    }

    private bool IsValidEmail(string email)
    {
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
        return emailRegex.IsMatch(email);
    }

    public async Task Register(CreateAccountDto account)
    {
        if (IsValidEmail(account.AccountEmail))
        {
            CreatePasswordHash(account.Password, out byte[] passwordHash, out byte[] passwordSalt);

            _account = new Account();
            string email = _accountRepository.Base64Encode(account.AccountEmail);

            _account.AccountEmail = email;
            _account.PasswordHash = passwordHash;
            _account.PasswordSalt = passwordSalt;
            _account.Owner = account.Owner;
            _account.Image = account.Image;
            _account.Status = true;
            _account.RoleID = account.RoleID;

            _context.Accounts.Add(_account);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new BadHttpRequestException("Not valid email!");
        }
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    public string CreateToken(Account account)
    {
        List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.Email, account.AccountEmail),
            new Claim(ClaimTypes.PostalCode, account.AccountID + ""),
            new Claim(ClaimTypes.Role, account.Role.RoleName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddHours(7),
            Created = DateTime.Now
        };
        return refreshToken;
    }

    public Account SetRefreshToken(RefreshToken newRefreshToken, HttpResponse response)
    {
        _account = new Account();
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        _account.RefreshToken = newRefreshToken.Token;
        _account.TokenCreated = newRefreshToken.Created;
        _account.TokenExpires = newRefreshToken.Expires;

        return _account;
    }

    public async Task<JWTDto> AuthenFirebase(bool isNewUser, string idToken)
    {
        string key = "AIzaSyAwB1GD5SLBrIMuOjp6DrOUhNGjkUKPUz0";
        string jwt = "";
        JWTDto jwtDto = null;
        FirebaseToken decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
        string uid = decodedToken.Uid;
        var authenUser = new FirebaseAuthProvider(new FirebaseConfig(key));
        var authen = authenUser.GetUserAsync(idToken);
        User user = authen.Result;
        var tagetAccount = await _context.Accounts.Include(a => a.Role)
        .Where(a => a.AccountEmail == _accountRepository.Base64Encode(user.Email.ToLower()))
        .FirstOrDefaultAsync();
        if (tagetAccount == null)
        {
            string url = "https://localhost:7091/Auth/email-verify/true&"+idToken+"";
            EmailDto emailDto = new EmailDto()
            {
                To = user.Email,
                Subject = "Chào mừng đến với Bookly :D",
                Body = "<p>Nhấn vào đường link để xác thực rằng bạn đã đăng kí tài khoàn Bookly với tài khoản Google của bạn</p> " + 
                        "<p><a href=" + url + ">Xác thực ở đây!!!</a></p>",
            };
            SendConfirmGoogleSignInEmail(emailDto);
            if (isNewUser == true)
            {
                CreateAccountDto createAccountDto = new CreateAccountDto()
                {
                    AccountEmail = user.Email.ToLower(),
                    Password = "",
                    Owner = user.DisplayName,
                    Image = user.PhotoUrl,
                    RoleID = 2
                };
                await Register(createAccountDto);
                return null;
            }else{
                return null;
            }
        }
        if (tagetAccount.Status == false)
        {
            throw new BadHttpRequestException("Your account have been block!");
        }
        String email = _accountRepository.Base64Decode(tagetAccount.AccountEmail);
        jwt = ReCreateFirebaseToken(tagetAccount, uid);
        jwtDto = new JWTDto(tagetAccount.AccountID, email, true, tagetAccount.Owner, user.PhotoUrl, jwt, tagetAccount.Role.RoleName);
        return (jwtDto);
    }

    public string ReCreateFirebaseToken(Account account, string uid)
    {
        if (account.AccountEmail != null)
        {
            List<Claim> claims = new List<Claim>{
            //new Claim(ClaimTypes.Name, account.Owner),
            new Claim(ClaimTypes.Email, account.AccountEmail),
            //new Claim(ClaimTypes.Uri, account.Image),
            new Claim(ClaimTypes.PostalCode, account.AccountID + ""),
            new Claim(ClaimTypes.Role, account.Role.RoleName),
            new Claim(ClaimTypes.GivenName, uid)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        else
        {
            throw new BadHttpRequestException("Fill all personal information");
        }
    }

    public void SendConfirmGoogleSignInEmail(EmailDto request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = request.Body
        };

        using var smtp = new SmtpClient();
        smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
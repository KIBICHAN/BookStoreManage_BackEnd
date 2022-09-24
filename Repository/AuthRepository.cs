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

namespace BookStoreManage.Repository;

public class AuthRepository : IAuthRepository{
    private Account _account = new Account();
    private readonly BookManageContext _context;
    private readonly IConfiguration _configuration;
    public AuthRepository(BookManageContext context, IConfiguration configuration){
        _context = context;
        _configuration = configuration;
    }

    public async Task<Account> Get(string username){
        var _acc = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.UserName == username);
        return _acc;
    }

    public async Task<Account> CheckLogin(AuthDto account){
        // var _acc = await Get(account.UserName);
        var _acc = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.UserName == account.UserName);
        if(account.UserName == _acc.UserName){
                if(!VerifyPasswordHash(account.Password, _acc.PasswordHash, _acc.PasswordSalt)){
                throw new BadHttpRequestException("Wrong password!");
            }
        }else{
            throw new BadHttpRequestException("No!");
        }
        return _acc;
    }

    public async Task Register(AuthDto account){
        CreatePasswordHash(account.Password, out byte[] passwordHash, out byte[] passwordSalt);

        _account = new Account();
        Role role = new Role();
        _account.UserName = account.UserName;
        _account.PasswordHash = passwordHash;
        _account.PasswordSalt = passwordSalt;
        _account.Status = true;

        _context.Accounts.Add(_account);
        await _context.SaveChangesAsync();
    }
    
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
        using(var hmac = new HMACSHA512()){
            passwordSalt =  hmac.Key;
            passwordHash =  hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt)){
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    public string CreateToken(Account account){
        List<Claim> claims = new List<Claim>{
            new Claim(ClaimTypes.Name, account.UserName),
            new Claim(ClaimTypes.Role, account.Role.RoleName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value));

        var cred = new  SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }

    public RefreshToken GenerateRefreshToken(){
        var refreshToken = new RefreshToken{
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };
        return refreshToken;
    }

    public Account SetRefreshToken(RefreshToken newRefreshToken, HttpResponse response){
        var cookieOptions = new CookieOptions{
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        _account.RefreshToken = newRefreshToken.Token;
        _account.TokenCreated = newRefreshToken.Created;
        _account.TokenExpires = newRefreshToken.Expires;

        return _account;
    }
}
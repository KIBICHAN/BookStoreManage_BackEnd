using Microsoft.AspNetCore.Mvc;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using BookStoreManage.DTO;
using Microsoft.AspNetCore.Authorization;
using FirebaseAdmin.Auth;

namespace BookStoreManage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class AuthController : ControllerBase
    {
        private static Account account = new Account();
        private readonly IAuthRepository _authRepository;
        private readonly BookManageContext _context;
        public AuthController(IAuthRepository authRepository, BookManageContext context)
        {
            _authRepository = authRepository;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register(AuthDto request)
        {
            try
            {
                await _authRepository.Register(request);
                return Ok(_context.Accounts.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(AuthDto request)
        {
            try
            {
                var acc = await _authRepository.CheckLogin(request);
                string token = _authRepository.CreateToken(acc);

                var refreshToken = _authRepository.GenerateRefreshToken();
                var setToken = _authRepository.SetRefreshToken(refreshToken, Response);

                account = acc;
                account.RefreshToken = setToken.RefreshToken;
                account.TokenExpires = setToken.TokenExpires;

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout"), Authorize]
        public ActionResult Logout()
        {
            try
            {
                account = null;
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public ActionResult<string> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (account.RefreshToken.Equals(refreshToken))
                {
                    return Unauthorized("Invalid Refresh Token.");
                }
                else if (account.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token expired.");
                }

                string token = _authRepository.CreateToken(account);
                var newRefreshToken = _authRepository.GenerateRefreshToken();
                var setToken = _authRepository.SetRefreshToken(newRefreshToken, Response);

                account.RefreshToken = setToken.RefreshToken;
                account.TokenExpires = setToken.TokenExpires;

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("verify-access-token")]
        public async Task<IActionResult> VerifyAccessToken(string accessToken)
        {
            try
            {
                var result = await _authRepository.AuthenFirebase(accessToken);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized("Account not valid!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("authen"), Authorize(Roles = "Staff"), Authorize(Roles = "Admin")]
        public ActionResult<Account> Authen()
        {
            return Ok(_context.Accounts.ToList());
        }
    }
}
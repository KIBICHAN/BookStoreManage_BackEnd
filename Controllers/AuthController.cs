using Microsoft.AspNetCore.Mvc;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using BookStoreManage.DTO;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class AuthController : ControllerBase
    {
        private static Account? account = new Account();
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
            await _authRepository.Register(request);
            return Ok(_context.Accounts.ToList());
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(AuthDto request)
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

        [HttpPost("logout"), Authorize]
        public ActionResult Logout(){
            account = null;
            return Ok();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(){
            var refreshToken = Request.Cookies["refreshToken"];
            if(!account.RefreshToken.Equals(refreshToken)){
                return Unauthorized("Invalid Refresh Token.");
            }else if(account.TokenExpires < DateTime.Now){
                return Unauthorized("Token expired.");
            }

            string token = _authRepository.CreateToken(account);
            var newRefreshToken = _authRepository.GenerateRefreshToken();
            var setToken = _authRepository.SetRefreshToken(newRefreshToken, Response);

            account.RefreshToken = setToken.RefreshToken;
            account.TokenExpires = setToken.TokenExpires;

            return Ok(token);
        }

        [HttpGet("authen"), Authorize(Roles = "Admin")]
        public ActionResult<Account> Authen()
        {
            return Ok(_context.Accounts.ToList());
        }
    }
}
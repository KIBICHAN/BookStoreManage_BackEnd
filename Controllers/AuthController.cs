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
        private readonly IAuthRepository _authRepository;
        private BookManageContext _context;
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
            return Ok(token);
        }

        [HttpGet("authen"), Authorize(Roles = "Admin")]
        public ActionResult<Account> Authen()
        {
            return Ok(_context.Accounts.ToList());
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using BookStoreManage.DTO;

namespace BookStoreManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Account>> Register(AccountDto request)
        {
            await _authRepository.Register(request);
            return Ok(_context.Accounts.ToList());
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(AccountDto request)
        {
            var acc = await _authRepository.Login(request);
            string token = _authRepository.CreateToken(acc);
            return Ok(token);
        }
    }
}
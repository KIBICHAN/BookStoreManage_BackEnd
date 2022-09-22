using Microsoft.AspNetCore.Mvc;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using BookStoreManage.Repository;
using DTO;

namespace BookStoreManage.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase{
        public static AccountModel user = new AccountModel();
        private readonly IAuthRepository authRepository;
        public AuthController(IAuthRepository authRepository){
            this.authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AccountModel>> Register(AccountDto request){
            authRepository.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.AccountName = request.AccountName;
            user.PasswordHash =  passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AccountDto request){
            if(user.AccountName != request.AccountName){
                return BadRequest("User not found.");
            }
            if(!authRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)){
                return BadRequest("Wrong password.");
            }

            return Ok("Here is your token!");
        }
    }
}
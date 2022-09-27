using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

    [Route("[controller]")]
    [ApiController]
    // [Authorize]
    public class AccountController : ControllerBase{
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository){
            _accountRepository = accountRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Account>>> GetAll(){
            var list = await _accountRepository.GetAll();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Account>> GetId(int id){
            var account = await _accountRepository.FindByID(id);
            return Ok(account);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditAccount(int id, AccountDto account){
            await _accountRepository.EditAccount(account, id);
            return Ok();
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult> ChangeStatus(int id, bool status){
            await _accountRepository.ChangeStatus(status,  id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(int id){
            await _accountRepository.DeleteAccount(id);
            return Ok();
        }
    }
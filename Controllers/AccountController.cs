using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
// [Authorize]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthRepository _authRepository;
    public AccountController(IAccountRepository accountRepository, IAuthRepository authRepository)
    {
        _accountRepository = accountRepository;
        _authRepository = authRepository;
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<Account>>> GetAll()
    {
        var list = await _accountRepository.GetAll();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].AccountAddress != null && list[i].Phone != null && list[i].AccountAddress != null)
            {
                list[i].AccountEmail = _accountRepository.Base64Decode(list[i].AccountEmail);
                list[i].Phone = _accountRepository.Base64Decode(list[i].Phone);
                list[i].AccountAddress = _accountRepository.Base64Decode(list[i].AccountAddress);
            }
        }

        return Ok(list);
    }

    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<List<Account>>> GetName(string name)
    {
        var list = await _accountRepository.GetName(name);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].AccountAddress != null && list[i].Phone != null && list[i].AccountAddress != null)
            {
                list[i].AccountEmail = _accountRepository.Base64Decode(list[i].AccountEmail);
                list[i].Phone = _accountRepository.Base64Decode(list[i].Phone);
                list[i].AccountAddress = _accountRepository.Base64Decode(list[i].AccountAddress);
            }
        }

        return Ok(list);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Account>> GetId(int id)
    {
        var account = await _accountRepository.FindByID(id);

        //account.AccountEmail = _accountRepository.Base64Decode(account.AccountEmail);

        return Ok(account);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Account>> Register(AuthDto request)
    {
        await _authRepository.Register(request);
        return Ok();
    }

    [HttpPut("Update/{id}")]
    public async Task<ActionResult> EditAccount(int id, AccountDto account)
    {
        await _accountRepository.EditAccount(account, id);
        return Ok();
    }

    [HttpPut("ChangeStatus/{id}")]
    public async Task<ActionResult> ChangeStatus(int id, bool status)
    {
        await _accountRepository.ChangeStatus(status, id);
        return Ok();
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        await _accountRepository.DeleteAccount(id);
        return Ok();
    }
}
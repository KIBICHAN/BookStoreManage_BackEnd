using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
// [Authorize(Roles = "Admin")]
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
        try
        {
            var list = await _accountRepository.GetAll();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AccountAddress != null)
                {
                    list[i].AccountAddress = _accountRepository.Base64Decode(list[i].AccountAddress);
                }
                if (list[i].Phone != null)
                {
                    list[i].Phone = _accountRepository.Base64Decode(list[i].Phone);
                }
                if (list[i].AccountEmail != null)
                {
                    list[i].AccountEmail = _accountRepository.Base64Decode(list[i].AccountEmail);
                }
            }
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<List<Account>>> GetName(string name)
    {
        try
        {
            var list = await _accountRepository.GetName(name);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AccountAddress != null)
                {
                    list[i].AccountAddress = _accountRepository.Base64Decode(list[i].AccountAddress);
                }
                if (list[i].Phone != null)
                {
                    list[i].Phone = _accountRepository.Base64Decode(list[i].Phone);
                }
                if (list[i].AccountEmail != null)
                {
                    list[i].AccountEmail = _accountRepository.Base64Decode(list[i].AccountEmail);
                }
            }
            return Ok(list);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Account>> GetId(int id)
    {
        try
        {
            var account = await _accountRepository.FindByID(id);
            if (account.AccountEmail != null)
            {
                account.AccountEmail = _accountRepository.Base64Decode(account.AccountEmail);
            }
            if (account.Phone != null)
            {
                account.Phone = _accountRepository.Base64Decode(account.Phone);
            }
            if (account.AccountAddress != null)
            {
                account.AccountAddress = _accountRepository.Base64Decode(account.AccountAddress);
            }
            return Ok(account);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Account>> Register(AuthDto request)
    {
        try
        {
            await _authRepository.Register(request);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<ActionResult> EditAccount(int id, AccountDto account)
    {
        try
        {
            await _accountRepository.EditAccount(account, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("ChangeStatus/{id}")]
    public async Task<ActionResult> ChangeStatus(int id, bool status)
    {
        try
        {
            await _accountRepository.ChangeStatus(status, id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> DeleteAccount(int id)
    {
        try
        {
            await _accountRepository.DeleteAccount(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
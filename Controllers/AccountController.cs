using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreManage.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthRepository _authRepository;
    public AccountController(IAccountRepository accountRepository, IAuthRepository authRepository)
    {
        _accountRepository = accountRepository;
        _authRepository = authRepository;
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<List<Account>>> GetName(string name)
    {
        try
        {
            var list = await _accountRepository.GetByName(name);
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

    [Authorize(Roles = "Admin,Staff")]
    [HttpGet("GetByRole/{roleId}")]
    public async Task<ActionResult<List<Account>>> GetByRole(int roleId)
    {
        try
        {
            var list = await _accountRepository.GetByRole(roleId);
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

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPost("Create")]
    public async Task<ActionResult<Account>> CreateAccount(CreateAccountDto request)
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

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPut("ChangeStatus")]
    public async Task<ActionResult> ChangeStatus(int id, ChangeStatusDto status)
    {
        try
        {
            await _accountRepository.ChangeStatus(id, status);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
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
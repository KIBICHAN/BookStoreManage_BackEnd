#nullable disable
using BookStoreManage.DTO;
using BookStoreManage.Entity;
using BookStoreManage.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManage.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly BookManageContext _context;
    public AccountRepository(BookManageContext context)
    {
        _context = context;
    }

    public async Task<List<Account>> GetAll()
    {
        var accountList = await _context.Accounts.Include(a => a.Orders).ToListAsync();
        return accountList;
    }

    public async Task<Account> FindByID(int id)
    {
        var account = await _context.Accounts.Include(a => a.Orders).FirstOrDefaultAsync(a => a.AccountID == id);
        return account;
    }

    public async Task EditAccount(AccountDto _account, int id)
    {
        var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);

        acc.Owner = _account.Owner;
        acc.AccountEmail = _account.AccountEmail;
        acc.Phone = _account.Phone;
        acc.AccountAddress = _account.AccountAddress;
        acc.Image = _account.Image;
        acc.Country = _account.Country;
        acc.Status = _account.Status;
        acc.RoleID = _account.RoleID;

        _context.Accounts.Update(acc);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAccount(int id)
    {
        var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);
        _context.Accounts.Remove(acc);
        await _context.SaveChangesAsync();
    }

    public async Task ChangeStatus(bool status, int id)
    {
        var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);
        
        acc.Status = status;

        _context.Accounts.Update(acc);
        await _context.SaveChangesAsync();
    }
}
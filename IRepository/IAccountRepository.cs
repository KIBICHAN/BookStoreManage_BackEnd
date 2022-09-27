using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAccountRepository{
    Task<List<Account>> GetAll();
    Task<Account> FindByID(int id);
    Task EditAccount (AccountDto _account, int id);
    Task DeleteAccount (int id);
    Task ChangeStatus(bool status, int id);
}
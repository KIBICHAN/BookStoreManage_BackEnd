using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAccountRepository{
    Task<List<Account>> GetAll();
    Task<List<Account>> GetName(string name);
    Task<Account> FindByID(int id);
    Task EditAccount (AccountDto _account, int id);
    Task DeleteAccount (int id);
    Task ChangeStatus(bool status, int id);
    string Base64Decode(string decodeStr);
}
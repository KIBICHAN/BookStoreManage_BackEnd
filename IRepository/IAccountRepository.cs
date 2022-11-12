using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAccountRepository{
    Task<List<Account>> GetAll();
    Task<List<Account>> GetByName(string name);
    Task<List<Account>> GetByRole(int roleId);
    Task<Account> FindByID(int id);
    Task EditAccount (AccountDto _account, int id);
    Task DeleteAccount (int id);
    Task ChangeStatus(int id, ChangeStatusDto status);
    string Base64Decode(string decodeStr);
    string Base64Encode(string textStr);
}
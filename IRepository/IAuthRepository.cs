using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAuthRepository{
    Task<Account> CheckLogin(AuthDto account);
    Task Register(AuthDto account);
    string CreateToken(Account account);
}   
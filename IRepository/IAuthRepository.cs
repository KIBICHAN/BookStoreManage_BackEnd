using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAuthRepository{
    Task<Account> Login(AccountDto account);
    Task Register(AccountDto account);
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt );
    string CreateToken(Account account);
}   
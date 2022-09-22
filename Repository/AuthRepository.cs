using Microsoft.EntityFrameworkCore;
using BookStoreManage.IRepository;
using BookStoreManage.Entity;
using System.Security.Cryptography;
using System.Text;

namespace BookStoreManage.Repository;

public class AuthRepository : IAuthRepository{
    public static AccountModel user = new AccountModel();
    private BookManageContext context;
    public AuthRepository(BookManageContext context){
        this.context = context;
    }
    
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt){
        using(var hmac = new HMACSHA512()){
            passwordSalt =  hmac.Key;
            passwordHash =  hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(user.PasswordSalt)){
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }
}
using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository;

public interface IAuthRepository{
    Task<Account> CheckLogin(AuthDto account);
    Task Register(CreateAccountDto account);
    string CreateToken(Account account);
    RefreshToken GenerateRefreshToken();
    Account SetRefreshToken(RefreshToken newRefreshToken, HttpResponse response);
    Task<JWTDto> AuthenFirebase(bool isNewUser, string idToken);
}   
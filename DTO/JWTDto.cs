#nullable disable
namespace BookStoreManage.DTO;

public class JWTDto
{
    public JWTDto(int accountId, string email, bool email_verified, bool is_new_user, string name, string picture, string jwt, string role)
    {
        this.is_new_user = is_new_user;
        this.accountId = accountId;
        this.email = email;
        this.email_verified = email_verified;
        this.name = name;
        this.picture = picture;
        this.jwt = jwt;
        this.role = role;
    }

    public int accountId { get; set; }
    public string email { get; set; }
    public bool email_verified { get; set; }
    public bool  is_new_user {get; set;}
    public string name { get; set; }
    public string picture { get; set; }
    public string jwt { get; set; }
    public string role { get; set; }
}
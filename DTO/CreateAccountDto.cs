#nullable disable
namespace BookStoreManage.DTO;

public class CreateAccountDto
{
    public string AccountEmail { get; set; }
    public string Password { get; set; }
    public int RoleID { get; set; }
}
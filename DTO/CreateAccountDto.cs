#nullable disable
namespace BookStoreManage.DTO;

public class CreateAccountDto
{
    public string AccountEmail { get; set; }
    public string Password { get; set; }

    public string Owner { get; set; }

    public string Phone { get; set; }

    public string AccountAddress { get; set; }

    public string Image { get; set; }

    public string Country { get; set; }

    public int RoleID { get; set; }
}
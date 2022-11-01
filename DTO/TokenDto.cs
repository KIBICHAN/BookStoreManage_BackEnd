#nullable disable
namespace BookStoreManage.DTO;

public class TokenDto
{
    public string AccountEmail { get; set; }
    public string Owner { get; set; }
    public string Token { get; set; }
    public string Image { get; set; }
    public string Role { get; set; }
}
#nullable disable

public class AccountModel{
    public string AccountName {get; set;}
    public byte[] PasswordHash {get; set;}
    public byte[] PasswordSalt {get; set;}
}
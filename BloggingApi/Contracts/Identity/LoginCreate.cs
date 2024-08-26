namespace BloggingApi.Contracts.Identity;

public class LoginCreate
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
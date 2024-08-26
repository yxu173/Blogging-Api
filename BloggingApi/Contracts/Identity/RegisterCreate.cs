namespace BloggingApi.Contracts.Identity;

public class RegisterCreate
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
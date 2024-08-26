namespace BloggingApi.Contracts.Identity;

public class UserUpdate
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
}
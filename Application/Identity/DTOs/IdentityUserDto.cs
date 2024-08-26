namespace Application.Identity.DTOs;

public class IdentityUserDto
{
    public required string UserName { get; set; }
    public required string EmailAddress { get; set; }
    public required string Password { get; set; }
    public required string Token { get; set; }
}
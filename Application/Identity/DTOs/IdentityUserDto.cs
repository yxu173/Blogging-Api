namespace Application.Identity.DTOs;

public record IdentityUserDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
}
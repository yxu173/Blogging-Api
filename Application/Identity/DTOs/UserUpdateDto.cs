namespace Application.Identity.DTOs;

public class UserUpdateDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
}
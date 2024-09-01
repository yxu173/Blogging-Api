using Application.Posts.DTOs;
using Domain.Entities;

namespace Application.Identity.DTOs;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
    public string Bio { get; set; }
    public string SocialMediaLinks { get; set; }
    public List<PostDto> Posts { get; set; } = new();
    
}
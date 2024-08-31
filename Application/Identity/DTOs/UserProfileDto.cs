using Application.Posts.DTOs;
using Domain.Entities;

namespace Application.Identity.DTOs;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    //public BasicInfoDto BasicInfo { get; set; }
    public List<PostDto> Posts { get; set; } = new();
   

    // public static UserProfileDto CreateUserProfileDto(User user, List<Post> posts)
    // {
    //     var userProfile = new UserProfileDto
    //     {
    //         Id = user.Id,
    //         UserName = user.UserName
    //     };
    //     posts.ForEach(p => userProfile.Posts.Add(PostDto.CreatePostDto(p)));
    //     return userProfile;
    // }
}
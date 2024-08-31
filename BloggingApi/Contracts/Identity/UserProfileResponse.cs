using Application.Identity.DTOs;
using BloggingApi.Contracts.Post.Response;
using Domain.Entities;

namespace BloggingApi.Contracts.Identity;

public class UserProfileResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public List<PostResponse> Posts { get; set; }

    // public static UserProfileResponse CreateUserProfileResponse(UserProfileDto user)
    // {
    //     var userProfile = new UserProfileResponse
    //     {
    //         Id = user.Id,
    //         UserName = user.UserName
    //     };
    //     user.Posts.ForEach(p => userProfile.Posts.Add(PostResponse.CreatePostDto(p)));
    //     return userProfile;
    // }
    
}
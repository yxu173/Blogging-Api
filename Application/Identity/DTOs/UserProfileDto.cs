using Application.Posts.DTOs;
using Domain.Entities;

namespace Application.Identity.DTOs;

public record UserProfileDto(
    Guid Id,
    string UserName,
    string ProfileImage,
    string Bio,
    string SocialMediaLinks,
    int FollowersCount,
    int FollowingCount,
    IReadOnlyList<PostDto> Posts);
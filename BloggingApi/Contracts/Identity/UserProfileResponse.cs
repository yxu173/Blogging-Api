using Application.Identity.DTOs;
using BloggingApi.Contracts.Post.Response;
using Domain.Entities;

namespace BloggingApi.Contracts.Identity;

public record UserProfileResponse(
    Guid Id,
    string UserName,
    string ProfileImage,
    string Bio,
    string SocialMediaLinks,
    IReadOnlyList<PostResponse> Posts);
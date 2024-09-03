using Application.Identity.DTOs;
using BloggingApi.Contracts.Post.Response;
using Domain.Entities;

namespace BloggingApi.Contracts.Identity;

public record UserProfileResponse(
    Guid Id,
    string UserName,
    List<PostResponse> Posts);
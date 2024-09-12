using Application.Posts.DTOs;
using Domain.Entities;

namespace BloggingApi.Contracts.Post.Response;

public record PostResponse(
    Guid Id,
    string Title,
    string Content,
    IReadOnlyList<Image> Images,
    Guid UserId,
    DateTime CreatedAt,
    int CommentCount,
    int LikeCount,
    IReadOnlyList<CommentResponse> Comments,
    IReadOnlyList<LikeResponse> Likes);
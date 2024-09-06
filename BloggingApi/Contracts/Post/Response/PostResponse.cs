using Application.Posts.DTOs;

namespace BloggingApi.Contracts.Post.Response;

public record PostResponse(
    Guid Id,
    string Title,
    string Content,
    Guid UserId,
    DateTime CreatedAt,
    int CommentCount,
    int LikeCount,
    IReadOnlyList<CommentResponse> Comments,
    IReadOnlyList<LikeResponse> Likes);
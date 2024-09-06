using Domain.Entities;

namespace Application.Posts.DTOs;

public record PostDto(
    Guid Id,
    string Title,
    string Content,
    Guid UserId,
    DateTime CreatedAt,
    int CommentCount,
    int LikeCount,
    IReadOnlyList<CommentDto> Comments,
    IReadOnlyList<LikeDto> Likes);
    
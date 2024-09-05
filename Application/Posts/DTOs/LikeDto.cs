using Domain.Entities;
using Domain.Enum;

namespace Application.Posts.DTOs;

public record LikeDto(
    Guid Id,
    Guid PostId,
    Guid UserId,
    string UserName,
    DateTime CreatedAt,
    InteractionType InteractionType);
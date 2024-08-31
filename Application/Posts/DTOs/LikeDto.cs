using Domain.Entities;
using Domain.Enum;

namespace Application.Posts.DTOs;

public class LikeDto
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public InteractionType InteractionType { get; set; }

    public static LikeDto CreateLikeDto(Like like)
    {
        return new LikeDto
        {
            Id = like.Id,
            PostId = like.PostId,
            UserId = like.UserId,
            CreatedAt = like.CreatedAt,
            InteractionType = like.InteractionType
        };
    }
}
using Application.Posts.DTOs;
using Domain.Entities;
using Domain.Enum;

namespace BloggingApi.Contracts.Post.Response;

public class LikeResponse
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public InteractionType InteractionType { get; set; }

    public static LikeResponse CreateLikeDto(LikeDto like)
    {
        return new LikeResponse
        {
            UserId = like.UserId,
            PostId = like.PostId,
            InteractionType = like.InteractionType
        };
    }
}
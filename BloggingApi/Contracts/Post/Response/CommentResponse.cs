using Application.Posts.DTOs;
using Domain.Entities;

namespace BloggingApi.Contracts.Post.Response;

public class CommentResponse
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; init; }

    public static CommentResponse CreateCommentDto(CommentDto comment)
    {
        return new CommentResponse
        {
            UserId = comment.UserId,
            PostId = comment.PostId,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt
        };
    }
}
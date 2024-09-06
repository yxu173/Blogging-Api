using Application.Posts.DTOs;
using Domain.Entities;

namespace BloggingApi.Contracts.Post.Response;

public record CommentResponse(Guid UserId, Guid PostId, string Content, DateTime CreatedAt);
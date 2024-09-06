using Application.Posts.DTOs;
using Domain.Entities;
using Domain.Enum;

namespace BloggingApi.Contracts.Post.Response;

public record LikeResponse(Guid UserId, Guid PostId, InteractionType InteractionType);
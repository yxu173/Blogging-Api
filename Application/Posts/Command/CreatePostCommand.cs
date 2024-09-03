using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public record CreatePostCommand(Guid UserId, string Title, string Content)
    : IRequest<OperationResult<Post>>;
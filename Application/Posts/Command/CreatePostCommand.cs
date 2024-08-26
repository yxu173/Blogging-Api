using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public class CreatePostCommand : IRequest<OperationResult<Post>>
{
    public required Guid UserId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}
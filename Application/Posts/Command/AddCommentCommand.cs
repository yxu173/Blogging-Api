using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public class AddCommentCommand : IRequest<OperationResult<CommentDto>>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public required string Content { get; set; }
}
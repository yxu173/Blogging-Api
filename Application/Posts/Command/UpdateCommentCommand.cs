using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public class UpdateCommentCommand : IRequest<OperationResult<CommentDto>>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public Guid CommentId { get; set; }
    public string Content { get; set; }
}
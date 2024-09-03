using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public record UpdateCommentCommand(Guid UserId, Guid PostId, Guid CommentId, string Content)
    : IRequest<OperationResult<CommentDto>>;
using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Command;

public record AddCommentCommand(Guid UserId, Guid PostId, string Content)
    : IRequest<OperationResult<CommentDto>>;
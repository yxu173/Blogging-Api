using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record DeleteCommentCommand(Guid UserId, Guid PostId, Guid CommentId) 
    : IRequest<OperationResult<bool>>;
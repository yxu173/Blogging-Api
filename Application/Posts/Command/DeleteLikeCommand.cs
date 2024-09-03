using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record DeleteLikeCommand(Guid UserId, Guid PostId, Guid LikeId) 
    : IRequest<OperationResult<bool>>;
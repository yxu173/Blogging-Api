using Application.Models;
using MediatR;

namespace Application.Follows.Command;

public record DeleteFollowCommand(Guid FollowerId, Guid FollowedId) : IRequest<OperationResult<bool>>;
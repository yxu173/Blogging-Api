using Application.Models;
using MediatR;

namespace Application.Follows.Command;

public record AddFollowCommand(Guid FollowerId, Guid FollowedId) : IRequest<OperationResult<bool>>;
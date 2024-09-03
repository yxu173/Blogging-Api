using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public record DeletePostCommand(Guid Id, Guid UserId) : IRequest<OperationResult<bool>>;
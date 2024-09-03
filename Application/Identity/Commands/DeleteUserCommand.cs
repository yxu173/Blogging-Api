using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<OperationResult<bool>>;
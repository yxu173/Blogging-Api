using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record LogoutUserCommand : IRequest<OperationResult<bool>>;
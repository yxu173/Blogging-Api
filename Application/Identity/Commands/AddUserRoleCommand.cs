using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public record AddUserRoleCommand(Guid UserId, string RoleName) : IRequest<OperationResult<bool>>;
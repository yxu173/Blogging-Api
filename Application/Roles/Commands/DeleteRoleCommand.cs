using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.UserRole.Commands;

public record DeleteRoleCommand(Guid Id) : IRequest<OperationResult<bool>>;
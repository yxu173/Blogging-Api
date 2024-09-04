using Application.Models;
using Application.Roles.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.UserRole.Commands;

public record CreateRoleCommand(string Name) : IRequest<OperationResult<RoleDto>>;



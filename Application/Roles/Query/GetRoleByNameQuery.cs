using Application.Models;
using Application.Roles.DTOs;
using MediatR;

namespace Application.Roles.Query;

public record GetRoleByNameQuery(string Name) : IRequest<OperationResult<RoleDto>>;
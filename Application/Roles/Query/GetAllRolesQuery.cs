using Application.Models;
using Application.Roles.DTOs;
using MediatR;

namespace Application.Roles.Query;

public record GetAllRolesQuery() : IRequest<OperationResult<IReadOnlyList<RoleDto>>>;
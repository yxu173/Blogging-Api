using Application.Enums;
using Application.Models;
using Application.Roles.DTOs;
using Application.Roles.Query;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.QueryHandler;

public class GetAllRolesHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, OperationResult<IReadOnlyList<RoleDto>>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<IReadOnlyList<RoleDto>> _result = new();

    public async Task< OperationResult<IReadOnlyList<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _context.Roles.ToListAsync(cancellationToken);
            if (roles.Count == 0)
            {
                _result.AddError(ErrorCode.RoleNotFound, "No roles found");
                return _result;
            }
            _result.Payload = _mapper.Map<IReadOnlyList<RoleDto>>(roles);
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, e.Message);
        }

        return _result;
    }
}
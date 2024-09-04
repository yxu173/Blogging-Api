using Application.Enums;
using Application.Models;
using Application.Roles.DTOs;
using Application.Roles.Query;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.QueryHandler;

public class GetRoleByNameHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetRoleByNameQuery, OperationResult<RoleDto>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<RoleDto> _result = new();

    public async Task<OperationResult<RoleDto>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                _result.AddError(ErrorCode.InvalidRoleName, "Invalid role name");
                return _result;
            }
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (role == null)
            {
                _result.AddError(ErrorCode.RoleDoesNotExist, "Role does not exist");
                return _result;
            }

            _result.Payload = _mapper.Map<RoleDto>(role);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}
using Application.Enums;
using Application.Exceptions.RoleExceptions;
using Application.Models;
using Application.Roles.DTOs;
using Application.UserRole.Commands;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Roles.CommandHandler;

public class CreateRoleHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, OperationResult<RoleDto>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<RoleDto> _result = new();

    public async Task<OperationResult<RoleDto>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                _result.AddError(ErrorCode.InvalidRoleName, "Invalid role name");
                return _result;
            }

            var isExist = _context.Roles.Any(x => x.Name == request.Name);
            if (isExist)
            {
                _result.AddError(ErrorCode.RoleAlreadyExists, "Role already exists");
                return _result;
            }

            var role = Role.Create(request.Name);
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _result.Payload = _mapper.Map<RoleDto>(role);
        }
        catch (RoleCreationEx e)
        {
            _result.AddError(ErrorCode.RoleCreationFailed, e.Message);
            return _result;
        }

        return _result;
    }
}


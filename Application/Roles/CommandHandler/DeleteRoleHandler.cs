using Application.Enums;
using Application.Exceptions.RoleExceptions;
using Application.Models;
using Application.UserRole.Commands;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRole.CommandHandler;

public class DeleteRoleHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteRoleCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _context = context;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (role == null)
            {
                _result.AddError(ErrorCode.RoleDoesNotExist, "Role does not exist");
                return _result;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (RoleDeletionEx e)
        {
            _result.AddError(ErrorCode.RoleDeletionFailed, e.Message);
        }

        return _result;
    }
}
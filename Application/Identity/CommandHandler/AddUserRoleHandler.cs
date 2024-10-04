using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.CommandHandler;

public class AddUserRoleHandler(ApplicationDbContext dbContext, UserServices userServices)
    : IRequestHandler<AddUserRoleCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserServices _userServices = userServices;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        AddUserRoleCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                x => x.Id == request.UserId,
                cancellationToken
            );

            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");
                return _result;
            }

            var role = await _dbContext.Roles.FirstOrDefaultAsync(
                x => x.Name == request.RoleName,
                cancellationToken
            );

            if (role == null)
            {
                _result.AddError(ErrorCode.RoleNotFound, "Role Not Found");
                return _result;
            }

            await _userServices.AddRoleToUser(user, request.RoleName);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.RoleNotFound, e.Message);
            return _result;
        }

        return _result;
    }
}


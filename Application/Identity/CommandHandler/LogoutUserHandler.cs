using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using MediatR;

namespace Application.Identity.CommandHandler;

public class LogoutUserHandler(UserServices userService) : IRequestHandler<LogoutUserCommand, OperationResult<bool>>
{
    private readonly UserServices _userService = userService;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService.LogoutUser();
            _result.Payload = result.IsCompleted;
        }
        catch (LogoutUserEx e)
        {
            _result.AddError(ErrorCode.UserLogoutFailed, e.Message);
        }

        return _result;
    }
}
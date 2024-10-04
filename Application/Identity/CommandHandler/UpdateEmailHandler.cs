using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateEmailHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<UpdateEmailCommand, OperationResult<bool>>
{
    private readonly UserServices _userService = userService;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        UpdateEmailCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _userService.GetUserById(request.Id);
            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");
                return _result;
            }

            if (request.EmailAddress == user.Email)
            {
                _result.AddError(ErrorCode.UpdateEmailFailed, "Email Already Exists");
                return _result;
            }

            var result = await _userService.UpdateEmail(request.Id, request.EmailAddress);
            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            _result.Payload = true;
        }
        catch (UpdateEmailEx e)
        {
            _result.AddError(ErrorCode.UpdateEmailFailed, e.Message);
            return _result;
        }

        return _result;
    }
}


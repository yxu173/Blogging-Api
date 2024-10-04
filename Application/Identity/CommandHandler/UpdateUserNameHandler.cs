using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateUserNameHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<UpdateUserNameCommand, OperationResult<bool>>
{
    private readonly UserServices _userService = userService;
    private IMapper _mapper = mapper;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        UpdateUserNameCommand request,
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

            if (request.UserName == user.UserName)
            {
                _result.AddError(ErrorCode.UpdateUsernameFailed, "Username Already Exists");
                return _result;
            }

            var result = await _userService.UpdateUserName(request.Id, request.UserName);

            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            _result.Payload = true;
        }
        catch (UpdateUsernameEx e)
        {
            _result.AddError(ErrorCode.UpdateUsernameFailed, e.Message);
            return _result;
        }

        return _result;
    }
}


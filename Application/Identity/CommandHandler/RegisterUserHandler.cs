using System.Security.Claims;
using System.Security.Principal;
using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Identity.CommandHandler;

public class RegisterUserHandler(UserServices userService, JwtService jwtService, IMapper mapper)
    : IRequestHandler<RegisterUserCommand, OperationResult<IdentityUserDto>>
{
    private OperationResult<IdentityUserDto> _result = new();

    public async Task<OperationResult<IdentityUserDto>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var isExisting = await userService.GetUserByEmail(request.Email);
            if (isExisting != null)
            {
                _result.AddError(ErrorCode.UserAlreadyExists, "User already exists");
                return _result;
            }

            var user = User.CreateUser(request.UserName, request.Email);
            var result = await userService.CreateUser(user, request.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                    _result.AddError(ErrorCode.UnknownError, error.Description));
                return _result;
            }

            _result.Payload = mapper.Map<IdentityUserDto>(user);
            _result.Payload.EmailAddress = user.Email;
            _result.Payload.UserName = user.UserName;
            _result.Payload.Token = GetToken(user);
        }
        catch (RegisterUserEx e)
        {
            e.ValidationErrors.ForEach(x => _result
                .AddError(ErrorCode.UserRegistrationFailed, e.Message));
        }

        return _result;
    }

    private string GetToken(User user)
    {
        var claims = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        });
        var token = jwtService.GenerateToken(claims);
        return token;
    }
}
using System.Security.Claims;
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

public class LoginUserHandler(UserServices userService, JwtService jwtService, IMapper mapper)
    : IRequestHandler<LoginUserCommand, OperationResult<IdentityUserDto>>
{
    private readonly UserServices _userService = userService;
    private readonly JwtService _jwtService = jwtService;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<IdentityUserDto> _result = new();

    public async Task<OperationResult<IdentityUserDto>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _userService.GetUserByUserName(request.UserName);
            var result = await _userService.LoginUser(request.UserName, request.Password);

            if (result.Succeeded)
            {
                _result.Payload = _mapper.Map<IdentityUserDto>(user);
                _result.Payload.Token = GetToken(user);
            }
            else
            {
                _result.AddError(ErrorCode.UnknownError, "Invalid login attempt.");
            }
        }
        catch (LoginUserEx e)
        {
            _result.AddError(ErrorCode.UserLoginFailed, e.Message);
            return _result;
        }

        return _result;
    }

    private string GetToken(User user)
    {
        var claims = new ClaimsIdentity(
            new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }
        );

        return _jwtService.GenerateToken(claims);
    }
}


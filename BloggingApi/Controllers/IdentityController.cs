using System.Security.Claims;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Identity.Query;
using BloggingApi.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

[AllowAnonymous]
public class IdentityController : BaseController
{
    [HttpPost]
    [Route(ApiRoute.User.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterCreate registerCreate)
    {
        var command = _mapper.Map<RegisterUserCommand>(registerCreate);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpPost]
    [Route(ApiRoute.User.Login)]
    public async Task<IActionResult> Login([FromBody] LoginCreate loginCreate)
    {
        var command = _mapper.Map<LoginUserCommand>(loginCreate);
        var result = await _mediator.Send(command);
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet]
    [Route(ApiRoute.User.GetByUserName)]
    [Authorize]
    public async Task<IActionResult> GetByUserName()
    {
        var result = await _mediator.Send(new GetUserByUserNameQuery
        {
            UserName = User.Identity?.Name
        });
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpDelete]
    [Route(ApiRoute.User.DeleteUser)]
    [Authorize]
    public async Task<IActionResult> DeleteById()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new DeleteUserCommand
        {
            Id = id
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost]
    [Route(ApiRoute.User.Logout)]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await _mediator.Send(new LogoutUserCommand());
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet]
    [Route(ApiRoute.User.GetById)]
    [Authorize]
    public async Task<IActionResult> GetById()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new GetUserByIdQuery
        {
            Id = id
        });
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpPost]
    [Route(ApiRoute.User.UpdateUser)]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new UpdateUserCommand
        {
            Id = id,
            UserName = userUpdateDto.UserName,
            Email = userUpdateDto.Email
        });
        var response = _mapper.Map<UserUpdate>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
}
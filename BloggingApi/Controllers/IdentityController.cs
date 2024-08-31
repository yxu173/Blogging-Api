using System.Security.Claims;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Identity.Query;
using BloggingApi.Contracts.Identity;
using Domain.Entities;
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
    [Route(ApiRoute.User.UpdateUsername)]
    [Authorize]
    public async Task<IActionResult> UpdateUsername([FromBody] UsernameUpdateDto usernameUpdateDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new UpdateUserNameCommand
        {
            Id = id,
            UserName = usernameUpdateDto.UserName
        });
        var response = _mapper.Map<UserUpdate>(result.Payload); //TODO: Edit DTO
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpPost]
    [Route(ApiRoute.User.UpdateEmail)]
    [Authorize]
    public async Task<IActionResult> UpdateUserEmail([FromBody] EmailUpdateDto emailUpdateDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new UpdateEmailCommand()
        {
            Id = id,
            EmailAddress = emailUpdateDto.EmailAddress
        });
        var response = _mapper.Map<UserUpdate>(result.Payload); //TODO: Edit DTO
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpPost]
    [Route(ApiRoute.User.CreateProfile)]
    [Authorize]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileDto basicInfo)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new CreateUserProfileCommand
        {
            Id = id,
            Bio = basicInfo.Bio,
            ProfileImage = basicInfo.ProfileImage,
            SocialMediaLinks = basicInfo.SocialMediaLinks
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost]
    [Route(ApiRoute.User.UpdateProfile)]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto basicInfo)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId.Value);
        var result = await _mediator.Send(new UpdateUserProfileCommand
        {
            Id = id,
            Bio = basicInfo.Bio,
            ProfileImage = basicInfo.ProfileImage,
            SocialMediaLinks = basicInfo.SocialMediaLinks
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet("GetUserProfileById/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserProfileById([FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new GetUserProfileQuery
        {
            UserId = userId
        });
        var response = _mapper.Map<UserProfileResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    
    
}
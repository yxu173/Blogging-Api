using Application.Identity.Commands;
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
        var result = await _mediator.Send(new RegisterUserCommand
        (
            registerCreate.UserName,
            registerCreate.Email,
            registerCreate.Password
        ));
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpPost]
    [Route(ApiRoute.User.VerifyEmail)]
    public async Task<IActionResult> VerifyEmail([FromBody] Guid Token)
    {
        var result = await _mediator.Send(new VerificationEmailCommand
        (
            Token
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
    

    [HttpPost]
    [Route(ApiRoute.User.Login)]
    public async Task<IActionResult> Login([FromBody] LoginCreate loginCreate)
    {
        var result = await _mediator.Send(new LoginUserCommand
        (
            loginCreate.UserName,
            loginCreate.Password
        ));
        var response = _mapper.Map<IdentityResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet("username")]
    [Authorize]
    public async Task<IActionResult> GetByUserName(string username)
    {
        var result = await _mediator.Send(new GetUserByUserNameQuery
        (
            username
        ));
        var response = _mapper.Map<UserProfileResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpDelete]
    [Route(ApiRoute.User.DeleteUser)]
    [Authorize]
    public async Task<IActionResult> DeleteById()
    {
        var result = await _mediator.Send(new DeleteUserCommand
        (
            UserId
        ));
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

    [HttpPost]
    [Route(ApiRoute.User.UpdateUsername)]
    [Authorize]
    public async Task<IActionResult> UpdateUsername([FromBody] string username) // TODO: Handle Username
    {
        var result = await _mediator.Send(new UpdateUserNameCommand
        (
            UserId,
            username
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost]
    [Route(ApiRoute.User.UpdateEmail)]
    [Authorize]
    public async Task<IActionResult> UpdateUserEmail([FromBody] string email)
    {
        var result = await _mediator.Send(new UpdateEmailCommand
        (
            UserId,
            email
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost]
    [Route(ApiRoute.User.UpdateProfile)]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto basicInfo)
    {
        var result = await _mediator.Send(new UpdateUserProfileCommand
        (
            UserId,
            basicInfo.Bio,
            basicInfo.SocialMediaLinks
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet("GetUserProfileById/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUserProfileById([FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new GetUserProfileQuery
        (
            userId
        ));
        var response = _mapper.Map<UserProfileResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpPost]
    [Route(ApiRoute.User.AddProfilePicture)]
    public async Task<IActionResult> AddProfilePicture([FromForm] PhotoDto file) // TODO: More Handle Profile Picture
    {
        var result = await _mediator.Send(new AddProfilePicCommand(
            UserId,
            file.File
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
}
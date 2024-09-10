using System.Security.Claims;
using Application.Follows.Command;
using Application.Follows.Query;
using BloggingApi.Contracts.Follow;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

[Authorize]
public class FollowController : BaseController
{
    [HttpPost]
    [Route("AddFollow/{followedId}")]
    public async Task<IActionResult> AddFollow([FromRoute] string followedId)
    {
        var result = await _mediator.Send(new AddFollowCommand
        (
            UserId,
            Guid.Parse(followedId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpDelete]
    [Route("RemoveFollow/{followedId}")]
    public async Task<IActionResult> RemoveFollow([FromRoute] string followedId)
    {
        var result = await _mediator.Send(new DeleteFollowCommand
        (
            UserId,
            Guid.Parse(followedId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet]
    [Route("GetAllFollowers")]
    public async Task<IActionResult> GetAllFollowers()
    {
        var result = await _mediator.Send(new GetAllFollowersQuery
        (
            UserId
        ));
        var response = _mapper.Map<List<FollowResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet]
    [Route("GetAllFollowing")]
    public async Task<IActionResult> GetAllFollowing()
    {
        var result = await _mediator.Send(new GetAllFollowingQuery
        (
            UserId
        ));
        var response = _mapper.Map<List<FollowResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
}
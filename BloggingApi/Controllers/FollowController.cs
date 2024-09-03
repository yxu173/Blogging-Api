using System.Security.Claims;
using Application.Follows.Command;
using Application.Follows.Query;
using BloggingApi.Contracts.Follow;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

public class FollowController : BaseController
{
    [HttpPost]
    [Route("AddFollow/{followedId}")]
    public async Task<IActionResult> AddFollow([FromRoute] string followedId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var result = await _mediator.Send(new AddFollowCommand
        (
            Guid.Parse(userId),
            Guid.Parse(followedId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpDelete]
    [Route("RemoveFollow/{followedId}")]
    public async Task<IActionResult> RemoveFollow([FromQuery] string followedId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var result = await _mediator.Send(new DeleteFollowCommand
        (
            Guid.Parse(userId),
            Guid.Parse(followedId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet]
    [Route("GetAllFollowers")]
    public async Task<IActionResult> GetAllFollowers()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var result = await _mediator.Send(new GetAllFollowersQuery
        (
            Guid.Parse(userId)
        ));
        var response = _mapper.Map<List<FollowResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet]
    [Route("GetAllFollowing")]
    public async Task<IActionResult> GetAllFollowing()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var result = await _mediator.Send(new GetAllFollowingQuery
        (
            Guid.Parse(userId)
        ));
        var response = _mapper.Map<List<FollowResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
}
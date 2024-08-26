using System.Security.Claims;
using Application.Posts.Command;
using Application.Posts.Query;
using BloggingApi.Contracts.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;


public class PostController : BaseController
{
    [HttpPost]
    [Route(ApiRoute.Post.CreatePost)]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] PostCreate postCreate)
    {
        var result = await _mediator.Send(new CreatePostCommand()
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Title = postCreate.Title,
            Content = postCreate.Content
        });
        var response = _mapper.Map<PostResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById([FromRoute] Guid postId)
    {
        var result = await _mediator.Send(new GetPostByIdQuery
        {
            Id = postId
        });
        var response = _mapper.Map<PostResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
}
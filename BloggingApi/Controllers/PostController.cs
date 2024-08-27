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
    [Authorize]
    public async Task<IActionResult> GetPostById([FromRoute] string postId) // TODO: Add Comments and liks to this
    {
        var result = await _mediator.Send(new GetPostByIdQuery
        {
            Id = Guid.Parse(postId),
        });
        var response = _mapper.Map<PostResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpDelete("DeletePost/{postId}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] string postId)
    {
        var result = await _mediator.Send(new DeletePostCommand
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Id = Guid.Parse(postId),
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPatch("UpdatePost/{postId}")]
    [Authorize]
    public async Task<IActionResult> UpdatePost([FromRoute] string postId, [FromBody] PostUpdate postUpdate)
    {
        var result = await _mediator.Send(new UpdatePostCommand
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Id = Guid.Parse(postId),
            Title = postUpdate.Title,
            Content = postUpdate.Content
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost("AddComment/{postId}")]
    [Authorize]
    public async Task<IActionResult> AddComment([FromRoute] string postId, [FromBody] AddComment addComment)
    {
        var result = await _mediator.Send(new AddCommentCommand
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            PostId = Guid.Parse(postId),
            Content = addComment.Content
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpDelete("DeleteComment/{postId}/{commentId}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] string postId, [FromRoute] string commentId)
    {
        var result = await _mediator.Send(new DeleteCommentCommand
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            PostId = Guid.Parse(postId),
            CommentId = Guid.Parse(commentId)
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPatch("UpdateComment/{postId}/{commentId}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment([FromRoute] string postId,
        [FromRoute] string commentId, [FromBody] AddComment updateComment)
    {
        var result = await _mediator.Send(new UpdateCommentCommand
        {
            UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            PostId = Guid.Parse(postId),
            CommentId = Guid.Parse(commentId),
            Content = updateComment.Content
        });
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
}
using System.Security.Claims;
using Application.Posts.Command;
using Application.Posts.Query;
using BloggingApi.Contracts.Post;
using BloggingApi.Contracts.Post.Request;
using BloggingApi.Contracts.Post.Response;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

[Authorize]
public class PostController : BaseController
{
    [HttpPost]
    [Route(ApiRoute.Post.CreatePost)]
    public async Task<IActionResult> CreatePost([FromBody] PostCreate postCreate)
    {
        var result = await _mediator.Send(new CreatePostCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            postCreate.Title,
            postCreate.Content
        ));
        var response = _mapper.Map<PostResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById([FromRoute] string postId) // TODO: Add Comments and liks to this
    {
        var result = await _mediator.Send(new GetPostByIdQuery
        (
            Guid.Parse(postId)
        ));
        var response = PostResponse.CreatePostDto(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpDelete("DeletePost/{postId}")]
    public async Task<IActionResult> DeletePost([FromRoute] string postId)
    {
        var result = await _mediator.Send(new DeletePostCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPatch("UpdatePost/{postId}")]
    public async Task<IActionResult> UpdatePost([FromRoute] string postId, [FromBody] PostUpdate postUpdate)
    {
        var result = await _mediator.Send(new UpdatePostCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            postUpdate.Title,
            postUpdate.Content
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost("AddComment/{postId}")]
    public async Task<IActionResult> AddComment([FromRoute] string postId, [FromBody] AddComment addComment)
    {
        var result = await _mediator.Send(new AddCommentCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            addComment.Content
        ));
        var mapped = _mapper.Map<CommentResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
    }

    [HttpDelete("DeleteComment/{postId}/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] string postId, [FromRoute] string commentId)
    {
        var result = await _mediator.Send(new DeleteCommentCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            Guid.Parse(commentId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPatch("UpdateComment/{postId}/{commentId}")]
    public async Task<IActionResult> UpdateComment([FromRoute] string postId,
        [FromRoute] string commentId, [FromBody] AddComment updateComment)
    {
        var result = await _mediator.Send(new UpdateCommentCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            Guid.Parse(commentId),
            updateComment.Content
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost("AddLike/{postId}")]
    public async Task<IActionResult> AddLike([FromRoute] string postId, [FromBody] LikeCreate likeCreate)
    {
        var result = await _mediator.Send(new AddLikeCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            likeCreate.InteractionType
        ));
        var mapped = _mapper.Map<LikeResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
    }

    [HttpDelete("DeleteLike/{postId}/{likeId}")]
    public async Task<IActionResult> DeleteLike([FromRoute] string postId, [FromRoute] string likeId)
    {
        var result = await _mediator.Send(new DeleteLikeCommand
        (
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            Guid.Parse(postId),
            Guid.Parse(likeId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet("GetPostCommentsByPostId/{postId}")]
    public async Task<IActionResult> GetPostCommentsByPostId([FromRoute] string postId)
    {
        var result = await _mediator.Send(new GetPostCommentsByPostIdQuery
        (
            Guid.Parse(postId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet("GetPostLikesByPostId/{postId}")]
    public async Task<IActionResult> GetPostLikesByPostId([FromRoute] string postId)
    {
        var result = await _mediator.Send(new GetPostLikesByPostIdQuery
        (
            Guid.Parse(postId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
}
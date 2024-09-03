using Application.PostTags.Command;
using Application.PostTags.Query;
using BloggingApi.Contracts.Tag;
using BloggingApi.Contracts.Tag.Request;
using BloggingApi.Contracts.Tag.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

public class TagController : BaseController
{
    [HttpPost]
    [Route(ApiRoute.Tag.CreateTag)]
    [Authorize]
    public async Task<IActionResult> CreateTag([FromBody] TagCreate tagCreate)
    {
        var result = await _mediator.Send(new CreateTagCommand
        (
            tagCreate.TagName
        ));
        var response = _mapper.Map<TagResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpDelete(ApiRoute.Tag.DeleteTag)]
    [Authorize]
    public async Task<IActionResult> DeleteTag([FromBody] TagCreate tagCreate)
    {
        var result = await _mediator.Send(new DeleteTagCommand
        (
            tagCreate.TagName
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet(ApiRoute.Tag.GetAllTags)]
    [Authorize]
    public async Task<IActionResult> GetAllTags()
    {
        var result = await _mediator.Send(new GetAllTagsQuery());
        var response = _mapper.Map<List<TagResponse>>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }

    [HttpGet(ApiRoute.Tag.GetPostsByTagName)]
    [Authorize]
    public async Task<IActionResult> GetPostsByTagName([FromBody] TagCreate tagCreate)
    {
        var result = await _mediator.Send(new GetAllPostsByTagNameQuery
        (
            tagCreate.TagName
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
}
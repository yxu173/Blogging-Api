using Application.Reports.Command;
using Application.Reports.Query;
using BloggingApi.Contracts.Report;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

[Authorize]
public class ReportController : BaseController
{
    [HttpPost]
    [Route(ApiRoute.Report.CreatePostReport)]
    public async Task<IActionResult> CreatePostReport([FromBody] ReportCreate reportCreate)
    {
        var result = await _mediator.Send(new CreateReportCommand
        (
            reportCreate.Id,
            ContentType.Post,
            reportCreate.Reason
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPost]
    [Route(ApiRoute.Report.CreateCommentReport)]
    public async Task<IActionResult> CreateCommentReport([FromBody] ReportCreate reportCreate)
    {
        var result = await _mediator.Send(new CreateReportCommand
        (
            reportCreate.Id,
            ContentType.Comment,
            reportCreate.Reason
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpPut]
    [Route("ResolveReport/{reportId}")]
    public async Task<IActionResult> ResolveReport([FromRoute] string reportId)
    {
        var result = await _mediator.Send(new ResolveReportCommand
        (
            Guid.Parse(reportId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet("GetReportById/{reportId}")]
    public async Task<IActionResult> GetReportById([FromRoute] string reportId)
    {
        var result = await _mediator.Send(new GetReportByIdQuery
        (
            Guid.Parse(reportId)
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [HttpGet]
    [Route(ApiRoute.Report.GetAllReports)]
    public async Task<IActionResult> GetAllReports()
    {
        var result = await _mediator.Send(new GetAllReportsQuery());
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
}
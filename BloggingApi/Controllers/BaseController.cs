using Application.Enums;
using Application.Models;
using AutoMapper;
using BloggingApi.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;
[Route(ApiRoute.BaseRoute)]
public class BaseController : ControllerBase
{
    private IMediator _mediatorInstance;
    private IMapper _mapperInstance;
    protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
    protected IMapper _mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
    
    protected ActionResult HandleErrorResponse(List<Error> errors)
    {
        var apiError = new ErrorResponse();
        if (errors.Any(e => e.Code == ErrorCode.NotFound))
        {
            var error = errors.FirstOrDefault(x => x.Code == ErrorCode.NotFound);
            apiError.StatusPhrase = "Not Found";
            apiError.StatusCode = 404;
            if (error != null)
                apiError.Errors.Add(error.Message);
            return NotFound(apiError);
        }

        apiError.StatusPhrase = "Bad Request";
        apiError.StatusCode = 400;
        errors.ForEach(x => apiError.Errors.Add(x.Message));
        return StatusCode(400, apiError);
    }
}
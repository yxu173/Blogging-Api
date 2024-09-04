using Application.Roles.Query;
using Application.UserRole.Commands;
using BloggingApi.Contracts.Role;
using BloggingApi.Contracts.Role.Response;
using Microsoft.AspNetCore.Mvc;

namespace BloggingApi.Controllers;

public class RoleController : BaseController
{
    [Route(ApiRoute.Role.CreateRole)]
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] CreateRole createRole)
    {
        var result = await _mediator.Send(new CreateRoleCommand
        (
            createRole.Name
        ));
        var response = _mapper.Map<RoleResponse>(result.Payload);
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(response);
    }
    
    [Route("DeleteRole/{id:guid}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteRole([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new DeleteRoleCommand
        (
            id
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [Route(ApiRoute.Role.GetAllRoles)]
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }

    [Route(ApiRoute.Role.GetRoleByName)]
    [HttpGet]
    
    public async Task<IActionResult> GetRoleByName([FromBody] CreateRole createRole)
    {
        var result = await _mediator.Send(new GetRoleByNameQuery
        (
            createRole.Name
        ));
        return result.IsError ? HandleErrorResponse(result.Errors) : Ok(result.Payload);
    }
    
}
using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Query;

public class GetUserByIdQuery : IRequest<OperationResult<IdentityUserDto>>
{
    public Guid Id { get; set; }
}
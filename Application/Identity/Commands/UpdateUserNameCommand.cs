using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class UpdateUserNameCommand : IRequest<OperationResult<UsernameUpdateDto>>
{
    public required Guid Id { get; set; }
    public required string UserName { get; set; }
}
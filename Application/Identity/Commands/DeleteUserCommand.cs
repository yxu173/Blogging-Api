using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class DeleteUserCommand : IRequest<OperationResult<bool>>
{
    public Guid Id { get; set; }
}
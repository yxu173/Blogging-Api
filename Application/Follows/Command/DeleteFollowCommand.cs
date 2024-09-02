using Application.Models;
using MediatR;

namespace Application.Follows.Command;

public class DeleteFollowCommand : IRequest<OperationResult<bool>>
{
    public Guid FollowerId { get; set; }
    public Guid FollowedId { get; set; }
}
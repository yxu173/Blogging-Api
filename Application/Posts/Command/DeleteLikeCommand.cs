using Application.Models;
using MediatR;

namespace Application.Posts.Command;

public class DeleteLikeCommand : IRequest<OperationResult<bool>>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public Guid LikeId { get; set; }
}
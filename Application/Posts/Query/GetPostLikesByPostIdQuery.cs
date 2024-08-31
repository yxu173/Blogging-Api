using Application.Models;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.Query;

public class GetPostLikesByPostIdQuery : IRequest<OperationResult<List<LikeDto>>>
{
    public Guid PostId { get; set; }
}
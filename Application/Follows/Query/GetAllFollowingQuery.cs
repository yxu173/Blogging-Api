using Application.Follows.DTOs;
using Application.Models;
using MediatR;

namespace Application.Follows.Query;

public class GetAllFollowingQuery : IRequest<OperationResult<List<UserFollowDto>>>
{
    public Guid UserId { get; set; }
}
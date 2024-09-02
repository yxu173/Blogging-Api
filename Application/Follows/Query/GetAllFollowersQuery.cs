using Application.Follows.DTOs;
using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Follows.Query;

public class GetAllFollowersQuery : IRequest<OperationResult<List<UserFollowDto>>>
{
    public Guid UserId { get; set; }
}
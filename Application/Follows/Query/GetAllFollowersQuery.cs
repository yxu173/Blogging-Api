using Application.Follows.DTOs;
using Application.Identity.DTOs;
using Application.Models;
using MediatR;

namespace Application.Follows.Query;

public record GetAllFollowersQuery(Guid UserId) : IRequest<OperationResult<List<UserFollowDto>>>;
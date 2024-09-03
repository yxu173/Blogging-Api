using Application.Follows.DTOs;
using Application.Models;
using MediatR;

namespace Application.Follows.Query;

public record GetAllFollowingQuery(Guid UserId) : IRequest<OperationResult<List<UserFollowDto>>>;
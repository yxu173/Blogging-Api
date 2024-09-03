using Application.Models;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.Query;

public record GetPostLikesByPostIdQuery(Guid PostId) : IRequest<OperationResult<List<LikeDto>>>;
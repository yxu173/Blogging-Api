using Application.Models;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.Query;

public record GetPostCommentsByPostIdQuery(Guid PostId)
    : IRequest<OperationResult<IReadOnlyList<CommentDto>>>;
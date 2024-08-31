using Application.Models;
using Application.Posts.DTOs;
using MediatR;

namespace Application.Posts.Query;

public class GetPostCommentsByPostIdQuery : IRequest<OperationResult<List<CommentDto>>>
{
    public Guid PostId { get; set; }
}
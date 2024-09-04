using Application.Enums;
using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.QueryHandler;

public class GetPostCommentsByPostIdHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetPostCommentsByPostIdQuery, OperationResult<IReadOnlyList<CommentDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<IReadOnlyList<CommentDto>> _result = new();

    public async Task<OperationResult<IReadOnlyList<CommentDto>>> Handle(GetPostCommentsByPostIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var comments  = _dbContext.Comments
                .Where(x => x.PostId == request.PostId)
                .Select(comment  =>
                    new CommentDto
                    {
                        Id = comment.Id,
                        PostId = comment.PostId,
                        UserId = comment.UserId,
                        UserName = comment.User.UserName,
                        Content = comment.Content,
                        CreatedAt = comment.CreatedAt
                    }).ToList();

            _result.Payload = comments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}
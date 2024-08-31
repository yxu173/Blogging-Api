using Application.Enums;
using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.QueryHandler;

public class GetPostCommentsByPostIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetPostCommentsByPostIdQuery, OperationResult<List<CommentDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper;
    private readonly OperationResult<List<CommentDto>> _result = new();

    public async Task<OperationResult<List<CommentDto>>> Handle(GetPostCommentsByPostIdQuery request,
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
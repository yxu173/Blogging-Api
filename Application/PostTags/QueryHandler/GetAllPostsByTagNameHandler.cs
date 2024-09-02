using Application.Enums;
using Application.Exceptions.TagExceptions;
using Application.Models;
using Application.Posts.DTOs;
using Application.PostTags.Query;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PostTags.QueryHandler;

public class GetAllPostsByTagNameHandler(ApplicationDbContext dbContext) :
    IRequestHandler<GetAllPostsByTagNameQuery, OperationResult<List<PostDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private OperationResult<List<PostDto>> _result = new();

    public async Task<OperationResult<List<PostDto>>> Handle(GetAllPostsByTagNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var posts = _dbContext.PostTags
                .AsNoTracking()
                .Include(pt => pt.Post)
                .Where(pt => pt.Tag.TagName == request.TagName)
                .Select(pt => pt.Post)
                .ToList();

            if (posts.Count == 0)
            {
                _result.AddError(ErrorCode.NotFound, "This Tag does not have any posts");
                return _result;
            }
            
            var postDtos = posts
                .Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    UserId = p.UserId,
                    CommentCount = p.CommentCount,
                    LikeCount = p.LikeCount,
                    Created = p.CreatedAt
                })
                .ToList();
            _result.Payload = postDtos;
        }
        catch (GetPostsByTagNameEx e)
        {
            _result.AddError(ErrorCode.UnknownError, "Something went wrong");
        }

        return _result;
    }
}
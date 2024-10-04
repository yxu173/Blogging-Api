using Application.Enums;
using Application.Exceptions.TagExceptions;
using Application.Models;
using Application.Posts.DTOs;
using Application.PostTags.Query;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PostTags.QueryHandler;

public class GetAllPostsByTagNameHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetAllPostsByTagNameQuery, OperationResult<IReadOnlyList<PostDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private OperationResult<IReadOnlyList<PostDto>> _result = new();

    public async Task<OperationResult<IReadOnlyList<PostDto>>> Handle(
        GetAllPostsByTagNameQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var posts = _dbContext
                .PostTags.AsNoTracking()
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
                .Select(p => new PostDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.UserId,
                    p.CreatedAt,
                    p.CommentCount,
                    p.LikeCount,
                    p.Comments.Select(c => new CommentDto(
                            c.Id,
                            c.PostId,
                            c.UserId,
                            c.User.UserName,
                            c.Content,
                            c.CreatedAt
                        ))
                        .ToList(),
                    p.Likes.Select(l => new LikeDto(
                            l.Id,
                            l.PostId,
                            l.UserId,
                            l.User.UserName,
                            l.CreatedAt,
                            l.InteractionType
                        ))
                        .ToList()
                ))
                .ToList();
            _result.Payload = postDtos;
        }
        catch (GetPostsByTagNameEx e)
        {
            _result.AddError(ErrorCode.UnknownError, "Something went wrong");
            return _result;
        }

        return _result;
    }
}


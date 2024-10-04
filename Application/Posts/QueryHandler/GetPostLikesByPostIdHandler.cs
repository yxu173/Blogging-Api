using Application.Enums;
using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using Infrastracture;
using MediatR;

namespace Application.Posts.QueryHandler;

public class GetPostLikesByPostIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetPostLikesByPostIdQuery, OperationResult<IReadOnlyList<LikeDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<IReadOnlyList<LikeDto>> _result = new();

    public Task<OperationResult<IReadOnlyList<LikeDto>>> Handle(
        GetPostLikesByPostIdQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var likes = _dbContext
                .Likes.Where(x => x.PostId == request.PostId)
                .Select(like => new LikeDto(
                    like.Id,
                    like.PostId,
                    like.UserId,
                    like.User.UserName,
                    like.CreatedAt,
                    like.InteractionType
                ))
                .ToList();

            _result.Payload = likes;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.LikeRetrievalFailed, e.Message);
            Task.FromResult(_result);
        }

        return Task.FromResult(_result);
    }
}


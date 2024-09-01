using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using Infrastracture;
using MediatR;

namespace Application.Posts.QueryHandler;

public class GetPostLikesByPostIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetPostLikesByPostIdQuery, OperationResult<List<LikeDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<List<LikeDto>> _result = new();

    public Task<OperationResult<List<LikeDto>>> Handle(GetPostLikesByPostIdQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var likes = _dbContext.Likes
                .Where(x => x.PostId == request.PostId)
                .Select(like =>
                    new LikeDto
                    {
                        Id = like.Id,
                        PostId = like.PostId,
                        UserId = like.UserId,
                        UserName = like.User.UserName,
                        InteractionType = like.InteractionType
                    }).ToList();

            _result.Payload = likes;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Task.FromResult(_result);
    }
}
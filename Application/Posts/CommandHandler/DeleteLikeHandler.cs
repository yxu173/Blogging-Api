using Application.Enums;
using Application.Exceptions.PostExceptions;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class DeleteLikeHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteLikeCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _dbContext.Posts
                .Include(x => x.Likes)
                .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post not found");
                return _result;
            }

            if (!post.Likes.Any(x => x.UserId == request.UserId))
            {
                _result.AddError(ErrorCode.AlreadyExists, "You didn't like this post");
                return _result;
            }

            var like = post.Likes.FirstOrDefault(x => x.Id == request.LikeId);
            if (like == null)
            {
                _result.AddError(ErrorCode.NotFound, "Like not found");
                return _result;
            }

            if (like.UserId != request.UserId)
            {
                _result.AddError(ErrorCode.LikeRemovalNotAuthorized,
                    "You are not authorized to remove this like");
                return _result;
            }
            post.RemoveLikeCounter();
            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (LikeDeletionEx e)
        {
            e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.LikeDeletionFailed, e.Message));
        }

        return _result;
    }
}
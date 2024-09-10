using Application.Enums;
using Application.Exceptions.FollowExceptions;
using Application.Follows.Command;
using Application.Models;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Follows.CommandHandler;

public class DeleteFollowHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteFollowCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteFollowCommand request
        , CancellationToken cancellationToken)
    {
        try
        {
            var follow = await _dbContext.Follows
                .FirstOrDefaultAsync(x => x.FollowerId == request.FollowerId &&
                                          x.FollowedId == request.FollowedId,
                    cancellationToken);

            if (follow == null)
            {
                _result.AddError(ErrorCode.NotFound, "Follow not found");
                return _result;
            }

            var follower = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.FollowerId, cancellationToken);

            var followed = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.FollowedId, cancellationToken);

            if (follower == null || followed == null)
            {
                _result.AddError(ErrorCode.NotFound, "User not found.");
                return _result;
            }

            follower.DecrementFollowingCount();
            followed.DecrementFollowersCount();

            _dbContext.Follows.Remove(follow);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (DeleteFollowEx e)
        {
            _result.AddError(ErrorCode.ServerError, e.Message);
        }

        return _result;
    }
}
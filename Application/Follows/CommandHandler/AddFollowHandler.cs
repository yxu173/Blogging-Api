using Application.Enums;
using Application.Exceptions.FollowExceptions;
using Application.Follows.Command;
using Application.Models;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Follows.CommandHandler;

public class AddFollowHandler(ApplicationDbContext dbContext) :
    IRequestHandler<AddFollowCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(AddFollowCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.FollowerId == request.FollowedId)
            {
                _result.AddError(ErrorCode.BadRequest, "You can't follow yourself");
                return _result;
            }

            var alreadyFollowing = await _dbContext.Follows
                .AnyAsync(x => x.FollowerId == request.FollowerId &&
                               x.FollowedId == request.FollowedId,
                    cancellationToken);
            if (alreadyFollowing)
            {
                _result.AddError(ErrorCode.AlreadyExists, "You already follow this user");
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

            follower.IncrementFollowingCount();
            followed.IncrementFollowersCount();

            var follow = Follow.CreateFollow(request.FollowerId, request.FollowedId);
            await _dbContext.Follows.AddAsync(follow, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.ServerError, e.Message);
        }

        return _result;
    }
}
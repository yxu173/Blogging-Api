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
            var followCheck = await _dbContext.Follows
                .Where(x => x.FollowerId == request.FollowerId &&
                            x.FollowedId == request.FollowedId)
                .FirstOrDefaultAsync(cancellationToken);
            if (followCheck != null)
            {
                _result.AddError(ErrorCode.AlreadyExists, "You already follow this user");
                return _result;
            }

            var follow = Follow.CreateFollow(request.FollowerId, request.FollowedId);
            await _dbContext.Follows.AddAsync(follow, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (AddFollowEx e)
        {
            _result.AddError(ErrorCode.ServerError, e.Message);
        }

        return _result;
    }
}
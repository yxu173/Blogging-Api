using Application.Enums;
using Application.Follows.DTOs;
using Application.Follows.Query;
using Application.Models;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Follows.QueryHandler;

public class GetAllFollowersHandler(ApplicationDbContext dbContext, IMapper mapper) :
    IRequestHandler<GetAllFollowersQuery, OperationResult<List<UserFollowDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<List<UserFollowDto>> _result = new();

    public async Task<OperationResult<List<UserFollowDto>>> Handle(GetAllFollowersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var followers = await _dbContext.Follows
                .AsNoTracking()
                .Where(x => x.FollowedId == request.UserId)
                .Select(x => new UserFollowDto
                {
                    UserName = x.Follower.UserName,
                    Bio = x.Follower.BasicInfo.Bio
                })
                .ToListAsync(cancellationToken);
            if (followers.Count == 0)
            {
                _result.AddError(ErrorCode.NotFound, "No followers found");
                return _result;
            }

            _result.Payload = _mapper.Map<List<UserFollowDto>>(followers);
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, "Something went wrong");
        }

        return _result;
    }
}
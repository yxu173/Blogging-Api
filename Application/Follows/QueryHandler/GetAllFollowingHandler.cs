using Application.Enums;
using Application.Follows.DTOs;
using Application.Follows.Query;
using Application.Models;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Follows.QueryHandler;

public class GetAllFollowingHandler(ApplicationDbContext dbContext, IMapper mapper) :
    IRequestHandler<GetAllFollowingQuery, OperationResult<List<UserFollowDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private OperationResult<List<UserFollowDto>> _result = new();

    public async Task<OperationResult<List<UserFollowDto>>> Handle(GetAllFollowingQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var following = await _dbContext.Follows
                .AsNoTracking()
                .Where(x => x.FollowerId == request.UserId)
                .Select(x => new UserFollowDto
                {
                    UserName = x.Followed.UserName,
                    Bio = x.Followed.BasicInfo.Bio
                })
                .ToListAsync(cancellationToken);
            if (following.Count == 0)
            {
                _result.AddError(ErrorCode.NotFound, "No following found");
                return _result;
            }

            _result.Payload = _mapper.Map<List<UserFollowDto>>(following);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}
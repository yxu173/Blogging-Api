using Application.Enums;
using Application.Identity.DTOs;
using Application.Identity.Query;
using Application.Models;
using Application.Posts.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.QueryHandler;

public class GetUserProfileHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserProfileQuery, OperationResult<UserProfileDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    private OperationResult<UserProfileDto> _result = new();

    public async Task<OperationResult<UserProfileDto>> Handle(GetUserProfileQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Where(x => x.Id == request.UserId)
                .Include(x => x.Posts)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Posts)
                .ThenInclude(x => x.Likes)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User not found");
                return _result;
            }

            var posts = user.Posts.ToList();

            var comments = new List<Comment>();
            var likes = new List<Like>();

            foreach (var post in posts)
            {
                comments.AddRange(post.Comments);
                likes.AddRange(post.Likes);
            }


            var userProfileDto = new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Posts = _mapper.Map<List<PostDto>>(posts)
            };

            _result.Payload = userProfileDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}
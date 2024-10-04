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

    private readonly OperationResult<UserProfileDto> _result = new();

    public async Task<OperationResult<UserProfileDto>> Handle(
        GetUserProfileQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _dbContext
                .Users.AsNoTracking()
                .Where(x => x.Id == request.UserId)
                .Select(u => new UserProfileDto(
                    u.Id,
                    u.UserName,
                    u.ProfileImage,
                    u.BasicInfo.Bio,
                    u.BasicInfo.SocialMediaLinks,
                    u.Followers.Count,
                    u.Following.Count,
                    u.Posts.Select(p => new PostDto(
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
                        .ToList()
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User not found");
                return _result;
            }

            _result.Payload = user;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.ServerError, "An unexpected error occurred");
            return _result;
        }

        return _result;
    }
}


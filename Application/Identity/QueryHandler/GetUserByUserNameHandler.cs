using Application.Enums;
using Application.Identity.DTOs;
using Application.Identity.Query;
using Application.Models;
using Application.Posts.DTOs;
using Application.Services;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.QueryHandler;

public class GetUserByUserNameHandler(UserServices userServices, ApplicationDbContext dbContext)
    : IRequestHandler<GetUserByUserNameQuery, OperationResult<UserProfileDto>>
{
    private readonly UserServices _userServices = userServices;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<UserProfileDto> _result = new();

    public async Task<OperationResult<UserProfileDto>> Handle(GetUserByUserNameQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.UserName == request.UserName)
            .Select(u => new UserProfileDto
            (
                u.Id,
                u.UserName,
                u.BasicInfo.ProfileImage,
                u.BasicInfo.Bio,
                u.BasicInfo.SocialMediaLinks,
                u.Posts.Select(p => new PostDto
                (
                    p.Id,
                    p.Title,
                    p.Content,
                    p.UserId,
                    p.CreatedAt,
                    p.CommentCount,
                    p.LikeCount,
                    p.Comments.Select(c => new CommentDto
                    (
                        c.Id,
                        c.PostId,
                        c.UserId,
                        c.User.UserName,
                        c.Content,
                        c.CreatedAt
                    )).ToList(),
                    p.Likes.Select(l => new LikeDto
                    (
                        l.Id,
                        l.PostId,
                        l.UserId,
                        l.User.UserName,
                        l.CreatedAt,
                        l.InteractionType
                    )).ToList()
                )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        _result.Payload = user;

        return _result;
    }
}
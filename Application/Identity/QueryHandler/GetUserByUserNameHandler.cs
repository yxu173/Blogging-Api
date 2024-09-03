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
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Created = p.CreatedAt,
                    Comments = p.Comments.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        PostId = c.PostId,
                        Content = c.Content,
                        CreatedAt = c.CreatedAt
                    }).ToList(),
                    Likes = p.Likes.Select(l => new LikeDto
                    {
                        Id = l.Id,
                        UserId = l.UserId,
                        PostId = l.PostId,
                        InteractionType = l.InteractionType
                    }).ToList()
                }).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        _result.Payload = user;

        return _result;
    }
}
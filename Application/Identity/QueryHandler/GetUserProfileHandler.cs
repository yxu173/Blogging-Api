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
        }

        return _result;
    }
}
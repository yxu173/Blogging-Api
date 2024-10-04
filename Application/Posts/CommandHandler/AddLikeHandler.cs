using Application.Enums;
using Application.Exceptions.PostExceptions;
using Application.Models;
using Application.Posts.Command;
using Application.Posts.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class AddLikeHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<AddLikeCommand, OperationResult<LikeDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<LikeDto> _result = new();

    public async Task<OperationResult<LikeDto>> Handle(
        AddLikeCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var post = await _dbContext
                .Posts.Include(x => x.Likes)
                .FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post not found");
                return _result;
            }

            if (post.Likes.Any(x => x.UserId == request.UserId))
            {
                _result.AddError(ErrorCode.AlreadyExists, "You already liked this post");
                return _result;
            }

            var like = Like.CreateLike(request.UserId, request.PostId, request.InteractionType);

            post.AddLikeCounter();
            await _dbContext.Likes.AddAsync(like, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _result.Payload = _mapper.Map<LikeDto>(like);
        }
        catch (AddLikeEx e)
        {
            e.ValidationErrors.ForEach(x =>
                _result.AddError(ErrorCode.LikeCreationFailed, e.Message)
            );
            return _result;
        }

        return _result;
    }
}


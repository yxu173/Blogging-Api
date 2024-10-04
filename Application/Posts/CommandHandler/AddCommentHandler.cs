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

public class AddCommentHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<AddCommentCommand, OperationResult<CommentDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<CommentDto> _result = new();

    public async Task<OperationResult<CommentDto>> Handle(
        AddCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(
                x => x.Id == request.PostId,
                cancellationToken
            );
            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post not found");
                return _result;
            }

            var comment = Comment.CreateComment(request.UserId, request.PostId, request.Content);
            post.AddCommentCounter();
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = _mapper.Map<CommentDto>(comment);
        }
        catch (AddCommentEx e)
        {
            e.ValidationErrors.ForEach(x =>
                _result.AddError(ErrorCode.CommentCreationFailed, e.Message)
            );
            return _result;
        }

        return _result;
    }
}


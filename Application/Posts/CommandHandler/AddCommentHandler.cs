using Application.Models;
using Application.Posts.Command;
using Application.Posts.DTOs;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Posts.CommandHandler;

public class AddCommentHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddCommentCommand, OperationResult<CommentDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<CommentDto> _result = new();

    public async Task<OperationResult<CommentDto>> Handle(AddCommentCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var comment = Comment.CreateComment(request.UserId, request.PostId, request.Content);
            await _dbContext.Comments.AddAsync(comment, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = new CommentDto
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
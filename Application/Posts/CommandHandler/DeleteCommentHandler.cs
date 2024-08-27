using Application.Enums;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class DeleteCommentHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteCommentCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = dbContext.Posts
                .Include(x => x.Comments)
                .Where(x => x.Id == request.PostId);
            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Comment Not Found");
                return _result;
            }

            var comment = post
                .SelectMany(x => x.Comments)
                .FirstOrDefault(x => x.Id == request.CommentId);
            
            if (comment == null)
            {
                _result.AddError(ErrorCode.NotFound, "Comment Not Found");
                return _result;
            }
            if (comment.UserId != request.UserId)
            {
                _result.AddError(ErrorCode.CommentRemovalNotAuthorized, 
                    "Comment Removal Not Authorized");
                return _result;
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
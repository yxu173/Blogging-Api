using Application.Enums;
using Application.Exceptions.PostExceptions;
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
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _dbContext.Posts
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == request.PostId
                    , cancellationToken);

            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");
                return _result;
            }


            var comment = post.Comments
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

            post.RemoveCommentCounter();
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (DeleteCommentEx e)
        {
            e.ValidationErrors.ForEach(x => _result.AddError(ErrorCode.CommentDeletionFailed, e.Message));
        }

        return _result;
    }
}
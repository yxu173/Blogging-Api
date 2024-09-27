using Application.Enums;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class RemoveTagFromPostHandler(ApplicationDbContext context)
    : IRequestHandler<RemoveTagFromPostCommand, OperationResult<bool>>
{
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(RemoveTagFromPostCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            var tag = await context.Tags.FirstOrDefaultAsync(x => x.Id == request.TagId, cancellationToken);
            if (post == null || tag == null)
            {
                _result.AddError(ErrorCode.NotFound, "Could not find post or tag");
                return _result;
            }

            post.RemoveTag(tag.Id);
            await context.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.ServerError, e.Message);
            return _result;
        }

        return _result;
    }
}
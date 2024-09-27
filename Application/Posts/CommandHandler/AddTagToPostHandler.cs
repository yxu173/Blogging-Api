using Application.Enums;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class AddTagToPostHandler(ApplicationDbContext _context)
    : IRequestHandler<AddTagToPostCommand, OperationResult<bool>>
{
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(AddTagToPostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.TagId, cancellationToken);
            if (post == null || tag == null)
            {
                _result.AddError(ErrorCode.NotFound,"Could not find post or tag");
                return _result;
            }
            post.AddTag(tag);
            await _context.SaveChangesAsync(cancellationToken);
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
using Application.Enums;
using Application.Exceptions.TagExceptions;
using Application.Models;
using Application.PostTags.Command;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PostTags.CommandHandler;

public class DeleteTagHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteTagCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        DeleteTagCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(
                x => x.TagName == request.TagName,
                cancellationToken
            );
            if (tag == null)
            {
                _result.AddError(ErrorCode.TagDoesNotExist, "Tag does not exist");
                return _result;
            }

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (DeleteTagEx e)
        {
            _result.AddError(ErrorCode.TagDeletionFailed, e.Message);
            return _result;
        }

        return _result;
    }
}


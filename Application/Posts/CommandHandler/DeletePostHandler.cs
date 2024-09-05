using Application.Enums;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class DeletePostHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeletePostCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Posts
                .Where(x => x.Id == request.Id)
                .Include(x => x.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");

                return _result;
            }

            _dbContext.Posts.RemoveRange(result);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.PostDeletionFailed, e.Message);
        }

        return _result;
    }
}
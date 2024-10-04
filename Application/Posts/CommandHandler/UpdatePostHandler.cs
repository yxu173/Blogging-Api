using Application.Enums;
using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.CommandHandler;

public class UpdatePostHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdatePostCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        UpdatePostCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await _dbContext
                .Posts.Where(x => x.Id == request.Id)
                .Include(x => x.UserId)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");
                return _result;
            }

            result.UpdatePost(request.Title, request.Content);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.PostUpdateFailed, e.Message);
            return _result;
        }

        return _result;
    }
}


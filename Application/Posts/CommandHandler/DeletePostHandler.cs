using Application.Models;
using Application.Posts.Command;
using Infrastracture;
using MediatR;

namespace Application.Posts.CommandHandler;

public class DeletePostHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeletePostCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Posts
                .FindAsync(request.Id);

            if (result != null)
                _dbContext.Posts.RemoveRange(result);
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
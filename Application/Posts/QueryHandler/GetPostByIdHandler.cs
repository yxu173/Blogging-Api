using Application.Enums;
using Application.Models;
using Application.Posts.Query;
using Application.Services;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Posts.QueryHandler;

public class GetPostByIdHandler(ApplicationDbContext dbContext, PostServices postServices)
    : IRequestHandler<GetPostByIdQuery, OperationResult<Post>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly PostServices _postServices = postServices;
    private OperationResult<Post> _result = new();
    public async Task<OperationResult<Post>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbContext.Posts
                .FindAsync(request.Id);

            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");
                return _result;
            }
            var htmlContent = _postServices.ConvertMarkdownToHtml(result.Content);
            result.UpdateContent(htmlContent);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = result;
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return _result;
    }
}
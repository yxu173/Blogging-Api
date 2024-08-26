using Application.Models;
using Application.Posts.Command;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Posts.CommandHandler;

public class CreatePostHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreatePostCommand, OperationResult<Post>>
{
    private ApplicationDbContext _dbContext = dbContext;
    private OperationResult<Post> _result = new();

    public async Task<OperationResult<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = Post.CreatePost(request.UserId, request.Title, request.Content);
            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();
            _result.Payload = post;
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
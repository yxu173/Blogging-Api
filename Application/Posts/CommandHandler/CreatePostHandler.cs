using Application.Enums;
using Application.Exceptions.PostExceptions;
using Application.Models;
using Application.Posts.Command;
using Application.Services;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Posts.CommandHandler;

public class CreatePostHandler(ApplicationDbContext dbContext, UploadPhotoServices uploadService)
    : IRequestHandler<CreatePostCommand, OperationResult<Post>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UploadPhotoServices _uploadService = uploadService;
    private readonly OperationResult<Post> _result = new();

    public async Task<OperationResult<Post>> Handle(CreatePostCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var post = Post.CreatePost(request.UserId, request.Title, request.Content);

            foreach (var image in request.Images)
            {
                var pic = await _uploadService.UploadPostPhoto(image, post.Id);
                post.AddImage(pic); // TODO: Add more handling for images 
            }

            await _dbContext.AddAsync(post, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = post;
        }
        catch (CreatePostEx e)
        {
            e.ValidationErrors.ForEach(x =>
                _result.AddError(ErrorCode.PostCreationFailed, e.Message));
        }

        return _result;
    }
}
using Application.Enums;
using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using Application.Services;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.QueryHandler;

public class GetPostByIdHandler(ApplicationDbContext dbContext, PostServices postServices)
    : IRequestHandler<GetPostByIdQuery, OperationResult<PostDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly PostServices _postServices = postServices;
    private OperationResult<PostDto> _result = new();

    public async Task<OperationResult<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _dbContext.Posts
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");
                return _result;
            }

            var comments = post.Comments.ToList();
            var likes = post.Likes.ToList();

            var htmlContent = _postServices.ConvertMarkdownToHtml(post.Content);
            post.UpdateContent(htmlContent);
            _result.Payload = PostDto.CreatePostDto(post, comments, likes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}
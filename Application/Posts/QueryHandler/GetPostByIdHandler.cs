using Application.Enums;
using Application.Models;
using Application.Posts.DTOs;
using Application.Posts.Query;
using Application.Services;
using AutoMapper;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.QueryHandler;

public class GetPostByIdHandler(ApplicationDbContext dbContext, PostServices postServices, IMapper mapper)
    : IRequestHandler<GetPostByIdQuery, OperationResult<PostDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly PostServices _postServices = postServices;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<PostDto> _result = new();

    public async Task<OperationResult<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _dbContext.Posts
                .AsNoTracking()
                .Select(p => new PostDto(
                    p.Id,
                    p.Title,
                    p.Content,
                    p.UserId,
                    p.CreatedAt,
                    p.CommentCount,
                    p.LikeCount,
                    p.Comments.Select(c => new CommentDto(
                        c.Id,
                        c.PostId,
                        c.UserId,
                        c.User.UserName,
                        c.Content,
                        c.CreatedAt
                    )).ToList(),
                    p.Likes.Select(l => new LikeDto(
                        l.Id,
                        l.PostId,
                        l.UserId,
                        l.User.UserName,
                        l.CreatedAt,
                        l.InteractionType
                    )).ToList()
                )).FirstOrDefaultAsync(cancellationToken);

            if (post == null)
            {
                _result.AddError(ErrorCode.NotFound, "Post Not Found");
                return _result;
            }

            //
            // var htmlContent = _postServices.ConvertMarkdownToHtml(post.Content);
            // post.UpdateContent(htmlContent);
            _result.Payload = _mapper.Map<PostDto>(post);
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.PostRetrievalFailed, e.Message);
        }

        return _result;
    }
}
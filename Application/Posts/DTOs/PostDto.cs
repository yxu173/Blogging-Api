using Domain.Entities;

namespace Application.Posts.DTOs;

public class PostDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public DateTime Created { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
    public List<CommentDto> Comments { get; set; } = new();
    public List<LikeDto> Likes { get; set; } = new();
    

    public static PostDto CreatePostDto(Post post, List<Comment> comments, List<Like> likes)
    {
        var response = new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CommentCount = post.CommentCount,
            LikeCount = post.LikeCount,
            UserId = post.UserId,
            Created = post.CreatedAt
        };
        comments.ForEach(c => 
            response.Comments.Add(CommentDto.CreateCommentDto(c)));
        likes.ForEach(l => 
            response.Likes.Add(LikeDto.CreateLikeDto(l)));
        return response;
    }
}
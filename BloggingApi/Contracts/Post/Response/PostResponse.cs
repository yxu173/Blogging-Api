using Application.Posts.DTOs;

namespace BloggingApi.Contracts.Post.Response;

public class PostResponse
{
    public string Title { get; init; }
    public string Content { get; init; }
    public Guid UserId { get; init; }
    public Guid PostId { get; init; }
    public DateTime CreatedAt { get; init; }
    public int CommentCount { get; init; }
    public int LikeCount { get; init; }
    public List<CommentResponse> Comments { get; init; } = new();
    public List<LikeResponse> Likes { get; init; } = new();

    public static PostResponse CreatePostDto(PostDto post)
    {
        if (post == null)
        {
            return null;
        }
        var response = new PostResponse
        {
            Title = post.Title,
            Content = post.Content,
            CommentCount = post.CommentCount,
            LikeCount = post.LikeCount,
            UserId = post.UserId,
            PostId = post.Id,
            CreatedAt = post.Created,
        };
        post.Comments.ForEach(c =>
            response.Comments.Add(CommentResponse.CreateCommentDto(c)));
        post.Likes.ForEach(l =>
            response.Likes.Add(LikeResponse.CreateLikeDto(l)));
        return response;
    }
}
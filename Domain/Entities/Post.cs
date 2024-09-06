using Domain.Common;
using Domain.Exceptions;
using Domain.Validators;

namespace Domain.Entities;

public sealed class Post : BaseAuditableEntity
{
    private readonly List<Image> _images = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Like> _likes = new();
    private readonly List<PostTag> _postTags = new();

    private Post() { }

    public Guid UserId { get; private init; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public int CommentCount { get; private set; }
    public int LikeCount { get; private set; }

    public User User { get; init; }

    public IEnumerable<Image> Images => _images;

    public IEnumerable<Comment> Comments => _comments;

    public IEnumerable<Like> Likes => _likes;

    public IEnumerable<PostTag> PostTags => _postTags;

    public static Post CreatePost(Guid userId, string title, string content)
    {
        var validator = new PostValidator();
        var validPost = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Content = content,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };
        var validationResult = validator.Validate(validPost);
        if (validationResult.IsValid) return validPost;
        var exception = new PostException("Post creation failed");
        validationResult.Errors
            .ToList()
            .ForEach(x => exception.ValidationErrors.Add(x.ErrorMessage));
        throw exception;
    }

    public void AddCommentCounter()
    {
        CommentCount++;
    }

    public void AddLikeCounter()
    {
        LikeCount++;
    }

    public void RemoveCommentCounter()
    {
        CommentCount--;
    }

    public void RemoveLikeCounter()
    {
        LikeCount--;
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }

    public void UpdatePost(string title, string content)
    {
        Title = title;
        Content = content;
    }
}
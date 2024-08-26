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
    private Post()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now.ToUniversalTime();
    }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }

    public User User { get; set; }

    public ICollection<Image> Images
    {
        get { return _images; }
    }

    public ICollection<Comment> Comments
    {
        get { return _comments; }
    }

    public ICollection<Like> Likes
    {
        get { return _likes; }
    }

    public ICollection<PostTag> PostTags
    {
        get { return _postTags; }
    }

    public static Post CreatePost(Guid userId, string title, string content)
    {
        var validator = new PostValidator();
        var validPost = new Post
        {
            UserId = userId,
            Title = title,
            Content = content
        };
        var validationResult = validator.Validate(validPost);
        if (validationResult.IsValid) return validPost;
        var exception = new PostException("Post creation failed");
        validationResult.Errors
            .ToList()
            .ForEach(x => exception.ValidationErrors.Add(x.ErrorMessage));
        throw exception;
    }

    public void UpdateContent(string content)
    {
        Content = content;
    }
}
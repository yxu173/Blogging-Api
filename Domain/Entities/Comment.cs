using Domain.Common;
using Domain.Exceptions;
using Domain.Validators;

namespace Domain.Entities;

public sealed class Comment : BaseAuditableEntity
{
    private readonly List<Comment> _replies = new();

    private Comment()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now.ToUniversalTime();
    }

    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public Guid? ParentCommentId { get; private set; } // Nullable for top-level comments
    public string Content { get; private set; }

    public User User { get; private set; }
    public Post Post { get; private set; }
    public Comment ParentComment { get; private set; }

    public ICollection<Comment> Replies
    {
        get { return _replies; }
    }

    public static Comment CreateComment(Guid userId, Guid postId, string content)
    {
        var validator = new CommentValidator();

        var validComment = new Comment
        {
            UserId = userId,
            PostId = postId,
            Content = content
        };
        var validationResult = validator.Validate(validComment);
        if (validationResult.IsValid) return validComment;
        var exception = new CommentException("Comment creation failed");
        validationResult.Errors
            .ToList()
            .ForEach(x => exception.ValidationErrors.Add(x.ErrorMessage));
        throw exception;
    }

    public void UpdateComment(string content)
    {
        Content = content;
        UpdatedAt = DateTime.Now.ToUniversalTime();
    }
}
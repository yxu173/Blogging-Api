using Domain.Common;

namespace Domain.Entities;

public sealed class Like : BaseEntity
{
    private Like()
    {
        Id = Guid.NewGuid();
    }
    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User User { get; private set; }
    public Post Post { get; private set; }

    public static Like CreateLike(Guid userId, Guid postId)
    {
        return new Like
        {
            UserId = userId,
            PostId = postId,
            CreatedAt = DateTime.Now
        };
    }
}
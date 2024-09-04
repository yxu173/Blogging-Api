using Domain.Common;
using Domain.Enum;

namespace Domain.Entities;

public sealed class Like : BaseEntity
{
    private Like()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now.ToUniversalTime();
    }

    public Guid UserId { get; private init; }
    public Guid PostId { get; private init; }
    public InteractionType InteractionType { get; private set; }
    public DateTime CreatedAt { get; private init; }

    public User User { get; private set; }
    public Post Post { get; private set; }

    public static Like CreateLike(Guid userId, Guid postId, InteractionType interactionType)
    {
        return new Like
        {
            UserId = userId,
            PostId = postId,
            InteractionType = interactionType
        };
    }
}
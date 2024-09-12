using Domain.Common;

namespace Domain.Entities;

public sealed class Image : BaseEntity
{
    private Image() { }

    public Guid PostId { get; private init; }
    public string ImageUrl { get; private set; }

    public Post Post { get; private init; }

    public static Image CreateImage(Guid postId, string imageUrl)
    {
        return new Image
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            ImageUrl = imageUrl
        };
    }
}
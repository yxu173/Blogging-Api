using Domain.Common;

namespace Domain.Entities;

public sealed class Image : BaseEntity
{
    private Image()
    {
        Id = Guid.NewGuid();
    }

    public Guid PostId { get; private set; }
    public string ImageUrl { get; private set; }
    public string ImageDescription { get; private set; }

    public Post Post { get; private set; }

    public static Image CreateImage(Guid postId, string imageUrl, string imageDescription)
    {
        return new Image
        {
            PostId = postId,
            ImageUrl = imageUrl,
            ImageDescription = imageDescription
        };
    }
}
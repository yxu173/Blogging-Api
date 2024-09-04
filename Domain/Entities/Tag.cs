using Domain.Common;

namespace Domain.Entities;

public sealed class Tag : BaseEntity
{
    private readonly List<PostTag> _postTags = new();

    private Tag()
    {
        Id = Guid.NewGuid();
    }

    public string TagName { get; private set; }

    public ICollection<PostTag> PostTags => _postTags;

    public static Tag CreateTag(string tagName)
    {
        return new Tag
        {
            TagName = tagName
        };
    }
}
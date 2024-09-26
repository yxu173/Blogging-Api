namespace Domain.Entities;

public sealed class PostTag
{
    private PostTag() { }
    public Guid PostId { get; private init; }
    public Guid TagId { get; private init; }

    public Post Post { get; private init; }
    public Tag Tag { get; private init; }

    public static PostTag Create( Guid postId, Guid tagId)
    {
        return new PostTag
        {
            PostId = postId,
            TagId = tagId
        };
    }

    
}
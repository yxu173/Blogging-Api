namespace Domain.Entities;

public sealed class PostTag
{
    public Guid PostId { get; private set; }
    public Guid TagId { get; private set; }

    public Post Post { get; private set; }
    public Tag Tag { get; private set; }
}
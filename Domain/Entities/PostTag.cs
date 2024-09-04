namespace Domain.Entities;

public sealed class PostTag
{
    public Guid PostId { get; private init; }
    public Guid TagId { get; private init; }

    public Post Post { get; private init; }
    public Tag Tag { get; private init; }
}
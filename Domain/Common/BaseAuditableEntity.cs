namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
}
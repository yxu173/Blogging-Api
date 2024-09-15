using Domain.Common;

namespace Domain.Entities;

public sealed class EmailVerificationToken : BaseAuditableEntity
{
    public DateTime ExpirationDate { get; private init; }
    public Guid UserId { get; private init; }
    public User User { get; private init; }

    public static EmailVerificationToken Create(DateTime expirationDate, Guid userId)
    {
        return new EmailVerificationToken
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow.ToUniversalTime(),
            ExpirationDate = expirationDate,
            UserId = userId
        };
    }
}
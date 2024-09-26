using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.Configrations;

public class EmailVerificationTokenConfigration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExpirationDate).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}
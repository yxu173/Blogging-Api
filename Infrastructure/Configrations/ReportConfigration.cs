using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.Configrations;

public class ReportConfigration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder
            .Property(r => r.ContentType)
            .HasConversion(
                v => v.ToString(),
                v => (ContentType)Enum.Parse(typeof(ContentType), v));
    }
}
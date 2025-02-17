using FinTrackAPI.Domain.Entities;
using FinTrackAPI.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrackAPI.Infrastructure.Persistence.Configurations;

public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable("investments");

        builder.HasKey(i => i.Uuid);

        builder.Property(i => i.Uuid)
            .HasColumnName("uuid")
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(i => i.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(i => i.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}

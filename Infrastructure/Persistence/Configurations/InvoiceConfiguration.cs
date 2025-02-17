using FinTrackAPI.Domain.Entities;
using FinTrackAPI.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrackAPI.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices");

        builder.HasKey(i => i.Uuid);

        builder.Property(i => i.Uuid)
            .HasColumnName("uuid")
            .IsRequired()
            .ValueGeneratedNever();

        builder.OwnsOne(i => i.Period, periodBuilder =>
        {
            periodBuilder.Property(p => p.Month)
                .HasColumnName("month")
                .IsRequired();

            periodBuilder.Property(p => p.Year)
                .HasColumnName("year")
                .IsRequired();
        });

        builder.Property(i => i.TotalAmount)
            .HasColumnName("total_amount")
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new MoneyConverter());

        builder.Property(i => i.IsPaid)
            .HasColumnName("is_paid")
            .IsRequired();

        builder.HasOne(i => i.CreditCard)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CreditCardUuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Expenses)
            .WithOne()
            .HasForeignKey(e => e.InvoiceUuid)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

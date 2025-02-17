using FinTrackAPI.Domain.Entities;
using FinTrackAPI.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrackAPI.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Uuid);

        builder.Property(u => u.Uuid)
            .HasColumnName("uuid")
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(u => u.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.BaseSalary)
            .HasColumnName("base_salary")
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new MoneyConverter());

        builder.Property(u => u.InvestmentPercentage)
            .HasColumnName("investment_percentage")
            .IsRequired()
            .HasPrecision(5, 2);

        builder.HasOne(u => u.EmergencyFund)
            .WithOne()
            .HasForeignKey<EmergencyFund>(e => e.UserUuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Incomes)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserUuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Expenses)
            .WithOne(e => e.Owner)
            .HasForeignKey(e => e.OwnerUuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.CreditCards)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserUuid)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Investments)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserUuid)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

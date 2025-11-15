using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Combo.Domain.Aggregates.Entities;

namespace RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Configuration;

public class ComboConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Entities.Combo>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Entities.Combo> builder)
    {
        builder.ToTable("combos");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");

        builder.Property(c => c.ProviderId)
            .HasColumnName("provider_id")
            .IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(c => c.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(c => c.InstallDays)
            .HasColumnName("install_days")
            .IsRequired();

        builder.Property(c => c.Image)
            .HasColumnName("image")
            .HasMaxLength(400)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


    }
}
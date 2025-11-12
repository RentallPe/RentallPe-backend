using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Aggregates.Entities;
using RentalPeAPI.Property.Domain.Aggregates.ValueObjects;

namespace RentalPeAPI.Property.Infrastructure.Persistence.EFCore.Configurations;

public class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.ToTable("Spaces");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(s => s.PricePerHour)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(s => s.Type)
            .HasConversion<string>()
            .IsRequired();

        // Value Object: Location
        builder.OwnsOne(s => s.Location, location =>
        {
            location.Property(l => l.Address)
                .HasColumnName("Address")
                .IsRequired()
                .HasMaxLength(255);
        });

        // Value Object: OwnerId
        builder.OwnsOne(s => s.OwnerId, owner =>
        {
            owner.Property(o => o.Value)
                .HasColumnName("OwnerId")
                .IsRequired();
        });
        

        // Configuración de Services
        builder.OwnsMany(s => s.Services, service =>
        {
            service.ToTable("SpaceServices");
            service.WithOwner().HasForeignKey("SpaceId");

            service.Property<int>("Id").ValueGeneratedOnAdd();
            service.HasKey("Id");

            service.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}

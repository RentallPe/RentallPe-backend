using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Aggregates.Entities;
using RentalPeAPI.Property.Domain.Aggregates.ValueObjects;

namespace RentalPeAPI.Property.Infrastructure.Persistence.EFC.Configuration;

public class SpaceConfiguration : IEntityTypeConfiguration<Space>
{
    public void Configure(EntityTypeBuilder<Space> builder)
    {
        builder.ToTable("spaces");

        // 1. Clave Principal: Definición limpia de la PK
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id"); 
        
        builder.Property(s => s.Name).IsRequired().HasMaxLength(120);
        builder.Property(s => s.Description).IsRequired().HasMaxLength(500);
        builder.Property(s => s.PricePerHour).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(s => s.Type).HasConversion<string>().IsRequired();

        // Value Object: Location
        builder.OwnsOne(s => s.Location, location =>
        {
            // [ARREGLO CRÍTICO] Mapeo a una tabla separada para evitar colisión de claves
            location.ToTable("space_locations"); 

            location.Property(l => l.Address)
                .HasColumnName("address")
                .IsRequired()
                .HasMaxLength(255);
        });

        // Value Object: OwnerId
        builder.OwnsOne(s => s.OwnerId, owner =>
        {
            // [ARREGLO CRÍTICO] Mapeo a una tabla separada
            owner.ToTable("space_owners"); 
            
            owner.Property(o => o.Value)
                .HasColumnName("owner_id") 
                .IsRequired();
        });

        // 4. Relación con Services
        builder.HasMany(s => s.Services)
            .WithOne()
            .HasForeignKey("space_id")
            .HasConstraintName("fk_spaces_services_space_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
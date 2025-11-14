// Monitoring/Infrastructure/Persistence/EFC/Configuration/ProjectConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        // Clave Primaria
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        
        // FKs (Usamos los tipos correctos: long y Guid)
        builder.Property(p => p.PropertyId).HasColumnName("property_id").IsRequired();
        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();

        // Propiedades y convenciones
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(255); 
        builder.Property(p => p.StartDate).HasColumnName("start_date");
        builder.Property(p => p.EndDate).HasColumnName("end_date");
        
        // Índices
        builder.HasIndex(p => p.PropertyId).IsUnique(); // Un espacio solo puede tener 1 proyecto activo a la vez
        builder.HasIndex(p => p.UserId); 
    }
}
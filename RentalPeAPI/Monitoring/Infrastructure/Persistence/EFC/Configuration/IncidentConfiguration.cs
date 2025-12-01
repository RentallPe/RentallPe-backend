// Monitoring/Infrastructure/Persistence/EFC/Configuration/IncidentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("incidents"); 

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("id").IsRequired();
        
        // FKs
        builder.Property(i => i.ProjectId).HasColumnName("project_id").IsRequired();
        builder.Property(i => i.IoTDeviceId).HasColumnName("iot_device_id").IsRequired();
        builder.Property(i => i.AcknowledgedByUserId).HasColumnName("acknowledged_by_user_id").IsRequired(false);

        // Propiedades
        builder.Property(i => i.Description).HasColumnName("description").HasMaxLength(500).IsRequired();
        builder.Property(i => i.Severity).HasColumnName("severity").HasMaxLength(20).IsRequired();
        builder.Property(i => i.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(i => i.ReportedAt).HasColumnName("reported_at");
        builder.Property(i => i.AcknowledgedAt).HasColumnName("acknowledged_at").IsRequired(false);
        builder.Property(i => i.ResolvedAt).HasColumnName("resolved_at").IsRequired(false);

        // Índices
        builder.HasIndex(i => i.ProjectId); 
        builder.HasIndex(i => i.IoTDeviceId); 
    }
}
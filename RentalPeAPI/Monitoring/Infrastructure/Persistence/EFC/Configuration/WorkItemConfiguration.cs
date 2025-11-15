// Monitoring/Infrastructure/Persistence/EFC/Configuration/TaskConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem> 
{
    public void Configure(EntityTypeBuilder<WorkItem> builder)
    {
        builder.ToTable("tasks"); // Nombre de la tabla

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id").IsRequired();
        
        // FKs
        builder.Property(t => t.ProjectId).HasColumnName("project_id").IsRequired();
        builder.Property(t => t.IncidentId).HasColumnName("incident_id").IsRequired(false); 
        builder.Property(t => t.AssignedToUserId).HasColumnName("assigned_to_user_id").IsRequired();

        // Propiedades
        builder.Property(t => t.Description).HasColumnName("description").HasMaxLength(500).IsRequired();
        builder.Property(t => t.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(t => t.CreatedAt).HasColumnName("created_at");
        builder.Property(t => t.CompletedAt).HasColumnName("completed_at").IsRequired(false);

        // Índices
        builder.HasIndex(t => t.ProjectId); 
        builder.HasIndex(t => t.IncidentId); 
        builder.HasIndex(t => t.AssignedToUserId); 
    }
}
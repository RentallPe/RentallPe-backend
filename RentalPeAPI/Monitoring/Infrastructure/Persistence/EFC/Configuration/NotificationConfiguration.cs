
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications"); 

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasColumnName("id").IsRequired();
        
        
        builder.Property(n => n.ProjectId).HasColumnName("project_id").IsRequired();
        builder.Property(n => n.IncidentId).HasColumnName("incident_id").IsRequired();

       
        builder.Property(n => n.Recipient).HasColumnName("recipient").HasMaxLength(150).IsRequired();
        builder.Property(n => n.Message).HasColumnName("message").HasMaxLength(1000).IsRequired();
        builder.Property(n => n.Type).HasColumnName("type").HasMaxLength(20).IsRequired();
        builder.Property(n => n.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(n => n.SentAt).HasColumnName("sent_at");

        
        builder.HasIndex(n => n.ProjectId); 
        builder.HasIndex(n => n.IncidentId); 
    }
}
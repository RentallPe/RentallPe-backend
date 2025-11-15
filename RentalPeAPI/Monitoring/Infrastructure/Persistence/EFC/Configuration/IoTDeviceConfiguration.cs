// Monitoring/Infrastructure/Persistence/EFC/Configuration/IoTDeviceConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class IoTDeviceConfiguration : IEntityTypeConfiguration<IoTDevice>
{
    public void Configure(EntityTypeBuilder<IoTDevice> builder)
    {
        builder.ToTable("iot_devices"); // Nombre de la tabla

        // PK
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").IsRequired();
        
        // FK a Project
        builder.Property(d => d.ProjectId).HasColumnName("project_id").IsRequired();

        // Propiedades
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(d => d.SerialNumber).HasColumnName("serial_number").HasMaxLength(50).IsRequired();
        builder.Property(d => d.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
        builder.Property(d => d.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(d => d.InstallationDate).HasColumnName("installation_date");

        // Índices
        builder.HasIndex(d => d.ProjectId); 
        builder.HasIndex(d => d.SerialNumber).IsUnique(); 
    }
}
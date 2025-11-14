// Monitoring/Infrastructure/Persistence/EFC/Configuration/ReadingConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

public class ReadingConfiguration : IEntityTypeConfiguration<Reading>
{
    public void Configure(EntityTypeBuilder<Reading> builder)
    {
        builder.ToTable("readings"); // Nombre de la tabla

        // PK (Usamos long para grandes volúmenes de datos)
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").IsRequired();
        
        // FK a IoTDevice
        builder.Property(r => r.IoTDeviceId).HasColumnName("iot_device_id").IsRequired();

        // Propiedades (Asegurando snake_case y restricciones)
        builder.Property(r => r.MetricName).HasColumnName("metric_name").HasMaxLength(50).IsRequired();
        builder.Property(r => r.Value).HasColumnName("value").HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(r => r.Unit).HasColumnName("unit").HasMaxLength(10).IsRequired();
        builder.Property(r => r.Timestamp).HasColumnName("timestamp").IsRequired();

        // Índices (Crucial para series temporales: buscar por dispositivo y tiempo)
        builder.HasIndex(r => r.IoTDeviceId); 
        builder.HasIndex(r => r.Timestamp); 
    }
}
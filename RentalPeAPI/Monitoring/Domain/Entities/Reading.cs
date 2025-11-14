// Monitoring/Domain/Entities/Reading.cs
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Reading
{
    public long Id { get; set; } // Usamos long para grandes volúmenes de datos
    
    // Clave Foránea a IoTDevice
    public int IoTDeviceId { get; set; } 
    public int ProjectId { get; set; }
    // Atributos de la Lectura
    public string MetricName { get; set; } = string.Empty; // Ej: 'Temperature', 'Pressure'
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty; // Ej: 'C', 'V', 'psi'
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public Reading() { }

    public Reading(int ioTDeviceId, int projectId, string metricName, decimal value, string unit, DateTime timestamp)
    {
        ProjectId = projectId;
        IoTDeviceId = ioTDeviceId;
        MetricName = metricName;
        Value = value;
        Unit = unit;
        Timestamp = timestamp;
    }
}
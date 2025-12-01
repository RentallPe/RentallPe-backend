
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Reading
{
    public long Id { get; set; } 
    
    
    public int IoTDeviceId { get; set; } 
    public int ProjectId { get; set; }
    
    public string MetricName { get; set; } = string.Empty; 
    public decimal Value { get; set; }
    public string Unit { get; set; } = string.Empty; 
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
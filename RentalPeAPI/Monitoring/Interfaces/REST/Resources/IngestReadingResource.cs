// Monitoring/Interfaces/REST/Resources/IngestReadingResource.cs
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json.Serialization;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

// Se usa la sintaxis de propiedad completa para permitir los atributos
public record IngestReadingResource
{
    [Required]
    [JsonPropertyName("deviceId")] 
    public int IoTDeviceId { get; init; } // Propiedad inmutable
    
    [Required] 
    [JsonPropertyName("projectId")] 
    public int ProjectId { get; init; }
    
    [Required]
    [JsonPropertyName("metricName")]
    public string MetricName { get; init; } = default!;
    
    [Required]
    [JsonPropertyName("value")]
    public decimal Value { get; init; }
    
    [Required]
    [JsonPropertyName("unit")]
    public string Unit { get; init; } = default!;
    
    [Required]
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; init; }

    // Constructor vacío requerido para la deserialización
    public IngestReadingResource() { } 

    // Constructor de mapeo (opcional, pero buena práctica)
    public IngestReadingResource(int ioTDeviceId,int projectId, string metricName, decimal value, string unit, DateTime timestamp)
    {
        
        IoTDeviceId = ioTDeviceId;
        ProjectId = projectId;
        MetricName = metricName;
        Value = value;
        Unit = unit;
        Timestamp = timestamp;
    }
}
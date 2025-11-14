// Monitoring/Domain/Entities/Incident.cs
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Incident
{
    // Claves Primarias y Foráneas
    public int Id { get; set; } 
    public int ProjectId { get; set; } 
    public int IoTDeviceId { get; set; } 
    public Guid? AcknowledgedByUserId { get; set; } // FK al usuario que lo revisó (User BC)

    // Atributos de la Alerta
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = "Medium"; 
    public string Status { get; set; } = "Reported"; // Reported, Acknowledged, Resolved
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcknowledgedAt { get; set; } 
    public DateTime? ResolvedAt { get; set; } 

    public Incident() { }

    // Constructor de creación
    public Incident(int projectId, int ioTDeviceId, string description, string severity)
    {
        ProjectId = projectId;
        IoTDeviceId = ioTDeviceId;
        Description = description;
        Severity = severity;
        Status = "Reported";
        ReportedAt = DateTime.UtcNow;
    }
    
    public void Acknowledge(Guid? userId)
    {
        // Esta es la lógica de negocio del dominio
        if (Status == "Reported")
        {
            Status = "Acknowledged";
            AcknowledgedAt = DateTime.UtcNow;
            AcknowledgedByUserId = userId; // Registra quién tomó acción
        }
    }
}

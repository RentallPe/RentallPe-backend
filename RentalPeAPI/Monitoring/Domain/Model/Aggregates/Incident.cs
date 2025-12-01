// Monitoring/Domain/Entities/Incident.cs
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Incident
{
    // En la BD puede ser identity/int; hacia el front será "id"
    public int Id { get; set; }

    // "projectId" en el JSON
    public int ProjectId { get; set; }

    // El dbjson no lo usa, pero lo mantenemos para dominio
    public int IoTDeviceId { get; set; }

    public Guid? AcknowledgedByUserId { get; set; }

    // "description" en el JSON
    public string Description { get; set; } = string.Empty;

    // Campo extra de dominio (el front no lo necesita)
    public string Severity { get; set; } = "Medium";

    // "status" en el JSON → por defecto "pending" como en el dbjson
    public string Status { get; set; } = "pending";

    // NUEVO: para mapear con "createdAt" del dbjson
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // NUEVO: para mapear con "updatedAt" del dbjson
    public DateTime? UpdatedAt { get; set; }

    // Campos antiguos que puedes seguir usando internamente
    public DateTime ReportedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcknowledgedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    public Incident() { }

    public Incident(int projectId, int ioTDeviceId, string description, string severity)
    {
        ProjectId = projectId;
        IoTDeviceId = ioTDeviceId;
        Description = description;
        Severity = severity;

        // Para front:
        Status = "pending";
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;

        // Si quieres seguir usando ReportedAt internamente:
        ReportedAt = CreatedAt;
    }

    public void Acknowledge(Guid? userId)
    {
        // Si estaba pendiente, lo marcamos como reconocido
        if (Status == "pending")
        {
            Status = "acknowledged";
            AcknowledgedAt = DateTime.UtcNow;
            UpdatedAt = AcknowledgedAt;
            AcknowledgedByUserId = userId;
        }
    }

    public void Resolve()
    {
        if (Status != "resolved")
        {
            Status = "resolved";
            ResolvedAt = DateTime.UtcNow;
            UpdatedAt = ResolvedAt;
        }
    }
}

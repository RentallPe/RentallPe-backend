// Monitoring/Domain/Entities/Task.cs
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class WorkItem
{
    public int Id { get; set; }
    
    // CLAVES FORÁNEAS
    public int ProjectId { get; set; } 
    public int? IncidentId { get; set; } 
    public int AssignedToUserId { get; set; } 

    // Atributos de la Tarea
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; } 

    // --- CONSTRUCTORES ---
    public WorkItem() { } // Constructor vacío para EF Core

    // Constructor de creación (Lógica de Dominio)
    public WorkItem(int projectId, int? incidentId, int assignedToUserId, string description)
    {
        // Aplicar validación de invariantes al crear el objeto
        if (projectId <= 0)
            throw new ArgumentException("Project ID is required.", nameof(projectId));

        ProjectId = projectId;
        IncidentId = incidentId;
        AssignedToUserId = assignedToUserId;
        Description = description;
        Status = "Pending"; // Invariante: El estado inicial es Pendiente
        CreatedAt = DateTime.UtcNow;
    }
}
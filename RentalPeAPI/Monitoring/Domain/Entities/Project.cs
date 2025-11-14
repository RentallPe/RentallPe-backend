// Monitoring/Domain/Entities/Project.cs
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Project
{
    // Clave Primaria del Project
    public int Id { get; set; } 

    // --- CLAVES FORÁNEAS (FK) ---
    // FK 1: Conexión al BC Space/Property
    public long PropertyId { get; set; } // <--- Usamos long para que coincida con Space.Id

    // FK 2: Conexión al BC User
    public Guid UserId { get; set; } // <--- Usamos Guid para que coincida con AppUser.Id
    // --- FIN DE FKS ---

    // Atributos del proyecto
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project() { }
    
    // Constructor de creación
    public Project(long propertyId, Guid userId, string name, string description, DateTime startDate, DateTime endDate)
    {
        PropertyId = propertyId;
        UserId = userId;
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Status = "Planning"; 
    }
}

using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Project
{
    
    public int Id { get; set; } 

    
    
    public long PropertyId { get; set; } 

    
    public Guid UserId { get; set; } 
    

    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } 
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project() { }
    
    
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
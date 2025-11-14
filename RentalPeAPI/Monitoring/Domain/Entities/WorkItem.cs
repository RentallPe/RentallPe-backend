
using System;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class WorkItem
{
    public int Id { get; set; }
    
    
    public int ProjectId { get; set; } 
    public int? IncidentId { get; set; } 
    public int AssignedToUserId { get; set; } 

   
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; } 

    
    public WorkItem() { } 

    
    public WorkItem(int projectId, int? incidentId, int assignedToUserId, string description)
    {
        
        if (projectId <= 0)
            throw new ArgumentException("Project ID is required.", nameof(projectId));

        ProjectId = projectId;
        IncidentId = incidentId;
        AssignedToUserId = assignedToUserId;
        Description = description;
        Status = "Pending"; 
        CreatedAt = DateTime.UtcNow;
    }
}
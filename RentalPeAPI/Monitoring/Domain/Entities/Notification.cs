
using System; 
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Monitoring.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    
    
    public int ProjectId { get; set; } 
    public int IncidentId { get; set; } 

   
    public string Recipient { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Email";
    public string Status { get; set; } = "Sent"; 
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public Notification() { }

   
    public Notification(int projectId, int incidentId, string recipient, string message, string type, string status)
    {
        ProjectId = projectId;
        IncidentId = incidentId;
        Recipient = recipient;
        Message = message;
        Type = type;
        Status = status;
        SentAt = DateTime.UtcNow;
    }
}
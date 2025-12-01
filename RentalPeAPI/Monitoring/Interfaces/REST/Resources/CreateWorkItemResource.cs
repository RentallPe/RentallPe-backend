// Monitoring/Interfaces/REST/Resources/CreateTaskResource.cs
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

public record CreateWorkItemResource(
    [Required] int ProjectId,
    int? IncidentId,
    [Required] int AssignedToUserId,
    [Required] string Description
);
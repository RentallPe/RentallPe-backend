using System;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

public record IncidentResource(
    int Id,
    int ProjectId,
    string Description,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
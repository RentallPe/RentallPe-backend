// Monitoring/Interfaces/REST/Resources/AcknowledgeIncidentResource.cs

namespace RentalPeAPI.Monitoring.Interfaces.REST.Resources;

public record AcknowledgeIncidentResource(
   Guid AcknowledgedByUserId
);
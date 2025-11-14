// Monitoring/Application/Internal/CommandServices/AcknowledgeIncidentCommand.cs
using MediatR;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public record AcknowledgeIncidentCommand(
    int IncidentId,
    Guid AcknowledgedByUserId // Usuario que reconoce el incidente
) : IRequest<bool>; // Devolverá true si fue exitoso
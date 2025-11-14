// Monitoring/Application/Internal/CommandServices/AcknowledgeIncidentCommandHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class AcknowledgeIncidentCommandHandler : IRequestHandler<AcknowledgeIncidentCommand, bool>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcknowledgeIncidentCommandHandler(IIncidentRepository incidentRepository, IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AcknowledgeIncidentCommand command, CancellationToken cancellationToken)
    {
        // 1. Obtener la entidad de la base de datos
        var incident = await _incidentRepository.FindByIdAsync(command.IncidentId);

        if (incident == null) return false; // El incidente no existe

        // 2. Ejecutar la lógica de dominio (cambiar el estado)
        incident.Acknowledge(command.AcknowledgedByUserId); 

        // 3. Guardar cambios
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
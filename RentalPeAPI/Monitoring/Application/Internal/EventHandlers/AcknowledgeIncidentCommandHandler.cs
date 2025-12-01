using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class AcknowledgeIncidentCommandHandler 
    : IRequestHandler<AcknowledgeIncidentCommand, bool>
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcknowledgeIncidentCommandHandler(
        IIncidentRepository incidentRepository, 
        IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        AcknowledgeIncidentCommand command, 
        CancellationToken cancellationToken)
    {
        // 1. Buscar el incidente
        var incident = await _incidentRepository.FindByIdAsync(command.IncidentId);
        if (incident is null) return false;

        // 2. Aplicar la lógica de dominio (cambia Status, UpdatedAt, etc.)
        incident.Acknowledge(command.AcknowledgedByUserId);

        // 3. Guardar cambios en BD
        await _unitOfWork.CompleteAsync();

        return true;
    }
}

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
       
        var incident = await _incidentRepository.FindByIdAsync(command.IncidentId);

        if (incident == null) return false; 

        
        incident.Acknowledge(command.AcknowledgedByUserId); 

        
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
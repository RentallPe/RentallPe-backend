// Monitoring/Application/Internal/CommandServices/IngestReadingCommandHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories; // Para IUnitOfWork
using RentalPeAPI.Monitoring.Domain.Services;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class IngestReadingCommandHandler : IRequestHandler<IngestReadingCommand>
{
    private readonly IReadingRepository _readingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAnomalyDetectorService _anomalyDetectorService;

    public IngestReadingCommandHandler(IReadingRepository readingRepository, IUnitOfWork unitOfWork, IAnomalyDetectorService anomalyDetectorService)
    {
        _readingRepository = readingRepository;
        _unitOfWork = unitOfWork;
        _anomalyDetectorService = anomalyDetectorService;
    }

    public async Task Handle(IngestReadingCommand command, CancellationToken cancellationToken)
    {
        // 1. Mapear el comando a la entidad de dominio
        var reading = new Reading(
            command.IoTDeviceId,
            command.ProjectId,
            command.MetricName,
            command.Value,
            command.Unit,
            command.Timestamp
        );

        // 2. Añadir al repositorio
        await _readingRepository.AddAsync(reading);

        // 3. Guardar cambios en la BD
        await _unitOfWork.CompleteAsync(); 
        await _anomalyDetectorService.CheckAndCreateIncidentAsync(reading);
        // (Aquí iría la lógica de 'Detect Anomaly' después de guardar)
    }
}
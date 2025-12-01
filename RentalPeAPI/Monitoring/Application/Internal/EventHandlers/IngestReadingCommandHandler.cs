// Monitoring/Application/Internal/CommandServices/IngestReadingCommandHandler.cs
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class IngestReadingCommandHandler : IRequestHandler<IngestReadingCommand, bool>
{
    private readonly IReadingRepository _readingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAnomalyDetectorService _anomalyDetectorService;

    public IngestReadingCommandHandler(
        IReadingRepository readingRepository,
        IUnitOfWork unitOfWork,
        IAnomalyDetectorService anomalyDetectorService)
    {
        _readingRepository = readingRepository;
        _unitOfWork = unitOfWork;
        _anomalyDetectorService = anomalyDetectorService;
    }

    public async Task<bool> Handle(IngestReadingCommand command, CancellationToken cancellationToken)
    {
        // Crear la entidad Reading a partir del comando
        var reading = new Reading(
            command.IoTDeviceId,
            command.ProjectId,
            command.MetricName,
            command.Value,
            command.Unit,
            command.Timestamp
        );

        // Guardar la lectura
        await _readingRepository.AddAsync(reading);

        // Confirmar cambios en BD
        await _unitOfWork.CompleteAsync();

        // Ejecutar lógica de detección de anomalías
        await _anomalyDetectorService.CheckAndCreateIncidentAsync(reading);

        // Si llegamos aquí, asumimos éxito
        return true;
    }
}
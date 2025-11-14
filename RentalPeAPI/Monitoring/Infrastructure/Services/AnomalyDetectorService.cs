// Monitoring/Infrastructure/Services/AnomalyDetectorService.cs
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;
using System.Threading.Tasks;
using System;

namespace RentalPeAPI.Monitoring.Infrastructure.Services;

public class AnomalyDetectorService : IAnomalyDetectorService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;
    public AnomalyDetectorService(IIncidentRepository incidentRepository, IUnitOfWork unitOfWork , INotificationService notificationService)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task CheckAndCreateIncidentAsync(Reading reading)
    {
       
        const decimal MAX_TEMP_THRESHOLD = 35.0m;
        
        if (reading.MetricName.Equals("Temperature", StringComparison.OrdinalIgnoreCase) && reading.Value > MAX_TEMP_THRESHOLD)
        {
         
            var incident = new Incident(
                reading.ProjectId, 
                reading.IoTDeviceId,
                $"Alerta: Temperatura ({reading.Value}°C) excedió el límite de {MAX_TEMP_THRESHOLD}°C.",
                "CRITICAL"
            );
            
          
            await _incidentRepository.AddAsync(incident);
            await _unitOfWork.CompleteAsync();
            await _notificationService.CreateNotificationForIncidentAsync(
                incident, 
                "gerencia@rentallpe.com");
            
        }
    }
}
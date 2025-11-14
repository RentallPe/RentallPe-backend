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

    public AnomalyDetectorService(IIncidentRepository incidentRepository, IUnitOfWork unitOfWork)
    {
        _incidentRepository = incidentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CheckAndCreateIncidentAsync(Reading reading)
    {
        // --- LÓGICA DE NEGOCIO Y REGLA DE UMBRAL ---
        const decimal MAX_TEMP_THRESHOLD = 35.0m;
        
        if (reading.MetricName.Equals("Temperature", StringComparison.OrdinalIgnoreCase) && reading.Value > MAX_TEMP_THRESHOLD)
        {
            // 1. Crear el incidente usando el ProjectId y DeviceId de la lectura
            var incident = new Incident(
                reading.ProjectId, 
                reading.IoTDeviceId,
                $"Alerta: Temperatura ({reading.Value}°C) excedió el límite de {MAX_TEMP_THRESHOLD}°C.",
                "CRITICAL"
            );
            
            // 2. Persistir el incidente
            await _incidentRepository.AddAsync(incident);
            await _unitOfWork.CompleteAsync();
            
            // (Aquí se llamaría al servicio de Notification después)
        }
    }
}
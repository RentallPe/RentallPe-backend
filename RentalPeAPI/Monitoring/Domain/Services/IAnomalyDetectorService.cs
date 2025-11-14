// Monitoring/Domain/Services/IAnomalyDetectorService.cs
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Domain.Services;

public interface IAnomalyDetectorService
{
    // Método que verifica si la lectura causa un incidente
    Task CheckAndCreateIncidentAsync(Reading reading);
}
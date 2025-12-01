// Monitoring/Domain/Services/IAnomalyDetectorService.cs
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Domain.Services;

public interface IAnomalyDetectorService
{
    
    Task CheckAndCreateIncidentAsync(Reading reading);
}
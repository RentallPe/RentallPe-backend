// Monitoring/Domain/Services/INotificationService.cs
using RentalPeAPI.Monitoring.Domain.Entities;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Domain.Services;

public interface INotificationService
{
    // Este servicio se encarga de crear el registro de notificación
    Task CreateNotificationForIncidentAsync(Incident incident, string recipient);
}
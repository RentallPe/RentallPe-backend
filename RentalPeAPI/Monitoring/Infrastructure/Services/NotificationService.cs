// Monitoring/Infrastructure/Services/NotificationService.cs
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateNotificationForIncidentAsync(Incident incident, string recipient)
    {
        // 1. Crear la entidad Notification
        var notification = new Notification(
            incident.ProjectId,
            incident.Id, // El ID del incidente
            recipient,
            $"Alerta Crítica: {incident.Description}",
            "Email",
            "Sent"
        );

        // 2. Persistir el registro
        await _notificationRepository.AddAsync(notification);
        await _unitOfWork.CompleteAsync();
    }
}
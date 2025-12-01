// Monitoring/Infrastructure/Services/NotificationService.cs
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

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
        // 👇 Por ahora usamos un userId genérico (0).
        // Cuando conectes con el BC de User, aquí podrás mapear el verdadero usuario destino.
        var userId = 0;

        var notification = new Notification(
            userId: userId,
            projectId: incident.ProjectId,
            message: $"Incidente reportado: {incident.Description}",
            incidentId: incident.Id,
            recipient: recipient,
            type: "Email",
            status: "Sent"
        );

        await _notificationRepository.AddAsync(notification);
        await _unitOfWork.CompleteAsync();
    }
}
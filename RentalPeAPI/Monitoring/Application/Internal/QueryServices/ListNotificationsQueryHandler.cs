// Monitoring/Application/Internal/QueryServices/ListNotificationsQueryHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Entities; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Application.Internal.QueryServices;

public class ListNotificationsQueryHandler 
    : IRequestHandler<ListNotificationsQuery, IEnumerable<Notification>>
{
    private readonly INotificationRepository _notificationRepository;

    public ListNotificationsQueryHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<IEnumerable<Notification>> Handle(ListNotificationsQuery query, CancellationToken cancellationToken)
    {
        // Esto asume que tienes un método ListByProjectId en tu repositorio
        // (Si no lo tienes, usa un ListAsync simple en la implementación)
        return await _notificationRepository.ListByProjectIdAsync(query.ProjectId);
    }
}
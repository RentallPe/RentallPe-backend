// Monitoring/Application/Internal/QueryServices/ListIoTDevicesByProjectQueryHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Application.Internal.QueryServices;

public class ListIoTDevicesByProjectQueryHandler 
    : IRequestHandler<ListIoTDevicesByProjectQuery, IEnumerable<IoTDevice>>
{
    private readonly IIoTDeviceRepository _deviceRepository;

    public ListIoTDevicesByProjectQueryHandler(IIoTDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<IEnumerable<IoTDevice>> Handle(ListIoTDevicesByProjectQuery query, CancellationToken cancellationToken)
    {
        // El repositorio filtra la lista por el ID del proyecto
        return await _deviceRepository.ListByProjectIdAsync(query.ProjectId);
    }
}
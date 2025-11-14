
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities; 

namespace RentalPeAPI.Monitoring.Application.Internal.QueryServices;

public record ListIoTDevicesByProjectQuery(int ProjectId) 
    : IRequest<IEnumerable<IoTDevice>>;
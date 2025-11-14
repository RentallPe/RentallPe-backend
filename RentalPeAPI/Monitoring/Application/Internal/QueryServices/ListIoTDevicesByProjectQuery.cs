// Monitoring/Application/Internal/QueryServices/ListIoTDevicesByProjectQuery.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities; // Asumo que el DTO de respuesta es la entidad

namespace RentalPeAPI.Monitoring.Application.Internal.QueryServices;

public record ListIoTDevicesByProjectQuery(int ProjectId) 
    : IRequest<IEnumerable<IoTDevice>>;
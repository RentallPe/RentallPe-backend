// Monitoring/Application/Internal/CommandServices/CreateIoTDeviceCommand.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities;

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public record CreateIoTDeviceCommand(
    int ProjectId,
    string Name,
    string SerialNumber,
    string Type
) : IRequest<IoTDevice>; 
// IRequest<IoTDevice> indica que el handler devolverá el objeto creado.
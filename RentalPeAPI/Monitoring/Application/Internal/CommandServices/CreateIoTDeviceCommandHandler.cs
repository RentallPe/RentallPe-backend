// Monitoring/Application/Internal/CommandServices/CreateIoTDeviceCommandHandler.cs
using MediatR;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories; // IUnitOfWork

namespace RentalPeAPI.Monitoring.Application.Internal.CommandServices;

public class CreateIoTDeviceCommandHandler : IRequestHandler<CreateIoTDeviceCommand, IoTDevice>
{
    private readonly IIoTDeviceRepository _deviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateIoTDeviceCommandHandler(IIoTDeviceRepository deviceRepository, IUnitOfWork unitOfWork)
    {
        _deviceRepository = deviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IoTDevice> Handle(CreateIoTDeviceCommand command, CancellationToken cancellationToken)
    {
        // 1. Crear la entidad usando el constructor
        var device = new IoTDevice(
            command.ProjectId,
            command.Name,
            command.SerialNumber,
            command.Type
        );

        // 2. Guardar en el repositorio (añadir al contexto de DB)
        await _deviceRepository.AddAsync(device);
        
        // 3. Persistir los cambios en la base de datos
        await _unitOfWork.CompleteAsync(); 

        return device; // Retorna el dispositivo recién creado (con su ID)
    }
}
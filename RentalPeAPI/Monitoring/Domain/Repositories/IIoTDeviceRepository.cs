// Monitoring/Domain/Repositories/IIoTDeviceRepository.cs
using RentalPeAPI.Monitoring.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IIoTDeviceRepository
{
    Task AddAsync(IoTDevice device);
    Task<IoTDevice?> FindByIdAsync(int id);
    Task<IEnumerable<IoTDevice>> ListByProjectIdAsync(int projectId);
}
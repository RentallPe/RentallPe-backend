// Monitoring/Domain/Repositories/IReadingRepository.cs
using RentalPeAPI.Monitoring.Domain.Entities;

using System.Threading.Tasks;
namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IReadingRepository
{
    // Task AddAsync (ahora se refiere a System.Threading.Tasks.Task)
    Task AddAsync(Reading reading);
    
    // Task<Reading?> FindLatestByDeviceIdAsync (ahora se refiere al tipo correcto)
    Task<Reading?> FindLatestByDeviceIdAsync(int deviceId);
}
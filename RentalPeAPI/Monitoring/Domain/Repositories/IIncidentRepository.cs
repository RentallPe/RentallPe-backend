// Monitoring/Domain/Repositories/IIncidentRepository.cs
using RentalPeAPI.Monitoring.Domain.Entities;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IIncidentRepository
{
    Task AddAsync(Incident incident);
    Task<Incident?> FindByIdAsync(int id);
    Task<IEnumerable<Incident>> ListByProjectAsync(int projectId);
}
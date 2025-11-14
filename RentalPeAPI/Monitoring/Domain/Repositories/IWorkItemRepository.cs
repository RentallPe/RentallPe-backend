// Monitoring/Domain/Repositories/ITaskRepository.cs
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;

// <-- ALIAS

namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IWorkItemRepository
{
    // Usamos el alias 'DomainTask' para la entidad
    Task AddAsync(WorkItem workItem); 
    Task<WorkItem?> FindByIdAsync(int id);
}
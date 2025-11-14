// Monitoring/Domain/Repositories/ITaskRepository.cs
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities;



namespace RentalPeAPI.Monitoring.Domain.Repositories;

public interface IWorkItemRepository
{
    
    Task AddAsync(WorkItem workItem); 
    Task<WorkItem?> FindByIdAsync(int id);
}
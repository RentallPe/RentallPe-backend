// Monitoring/Infrastructure/Persistence/EFC/Repositories/TaskRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Domain.Entities; 
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly AppDbContext _context;

    public WorkItemRepository(AppDbContext context)
    {
        _context = context;
    }

    
    public async Task AddAsync(WorkItem workItem)
    {
        await _context.Tasks.AddAsync(workItem);
    }

    
    public async Task<WorkItem?> FindByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }
}
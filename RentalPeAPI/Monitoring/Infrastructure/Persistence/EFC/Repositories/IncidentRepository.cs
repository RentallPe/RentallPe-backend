// Monitoring/Infrastructure/Persistence/EFC/Repositories/IncidentRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Incident incident)
    {
        await _context.Incidents.AddAsync(incident);
    }

    public async Task<Incident?> FindByIdAsync(int id)
    {
        return await _context.Incidents.FindAsync(id);
    }

    public async Task<IEnumerable<Incident>> ListByProjectAsync(int projectId)
    {
        return await _context.Incidents
            .Where(i => i.ProjectId == projectId)
            .OrderByDescending(i => i.CreatedAt) // opcional: más recientes primero
            .ToListAsync();
    }
}
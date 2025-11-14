// Monitoring/Infrastructure/Persistence/EFC/Repositories/IoTDeviceRepository.cs
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; // AppDbContext
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;

public class IoTDeviceRepository : IIoTDeviceRepository
{
    private readonly AppDbContext _context;

    public IoTDeviceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(IoTDevice device)
    {
        await _context.IoTDevices.AddAsync(device);
    }

    public async Task<IoTDevice?> FindByIdAsync(int id)
    {
        return await _context.IoTDevices.FindAsync(id);
    }

    public async Task<IEnumerable<IoTDevice>> ListByProjectIdAsync(int projectId)
    {
        return await _context.IoTDevices
            .Where(d => d.ProjectId == projectId)
            .ToListAsync();
    }
}
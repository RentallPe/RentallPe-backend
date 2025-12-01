using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Property.Infrastructure.Persistence.EFC.Repositories;

public class SpaceRepository : ISpaceRepository
{
    private readonly AppDbContext _context;

    public SpaceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Space space)
    {
        await _context.Spaces.AddAsync(space);
    }

    public async Task<Space?> FindByIdAsync(long id)
    {
        return await _context.Spaces
            .Include(s => s.Services)
            .FirstOrDefaultAsync(s => s.Id == id);
    }


    public async Task<IEnumerable<Space>> ListAsync(int? ownerId = null, string? type = null)
    {
        var query = _context.Spaces
            .Include(s => s.Services)
            .AsQueryable();

        if (ownerId.HasValue)
            query = query.Where(s => s.OwnerId.Value == ownerId.Value);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(s => s.Type.ToString() == type);

        return await query.ToListAsync();
    }

    public void Remove(Space space)
    {
        _context.Spaces.Remove(space);
    }

    
}
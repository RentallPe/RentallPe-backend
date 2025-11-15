using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Combo.Domain.Aggregates.Entities;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Repositories;

public class ComboRepository : IComboRepository
{
    private readonly AppDbContext _context;

    public ComboRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task AddAsync(Domain.Aggregates.Entities.Combo combo)
    {
        await _context.Combos.AddAsync(combo);
    }

    public async Task<Domain.Aggregates.Entities.Combo?> FindByIdAsync(int id)
    {
        return await _context.Combos.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Domain.Aggregates.Entities.Combo>> ListAsync(int? providerId = null)
    {
        var query = _context.Combos.AsQueryable();

        if (providerId.HasValue)
            query = query.Where(c => c.ProviderId == providerId.Value);

        return await query.ToListAsync();
    }

    public void Remove(Domain.Aggregates.Entities.Combo combo)
    {
        _context.Combos.Remove(combo);
    }

    // 👇 implementación de GetByIdAsync
    public async Task<Domain.Aggregates.Entities.Combo?> GetByIdAsync(int id)
    {
        return await _context.Combos.FindAsync(id);
    }


    // 👇 implementación de SaveChangesAsync
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
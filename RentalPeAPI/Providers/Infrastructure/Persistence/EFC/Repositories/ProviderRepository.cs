using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Repositories;

namespace RentalPeAPI.providers.Infrastructure.Persistence.EFC.Repositories;

public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
{
    public ProviderRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(Provider provider)
    {
        await Context.Set<Provider>().AddAsync(provider);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Provider provider)
    {
        Context.Set<Provider>().Update(provider);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Provider provider)
    {
        Context.Set<Provider>().Remove(provider);
        await Context.SaveChangesAsync();
    }

    public async Task<Provider?> FindByIdAsync(int id)
    {
        return await Context.Set<Provider>().FindAsync(id);
    }

    public async Task<IEnumerable<Provider>> FindAllAsync()
    {
        return await Context.Set<Provider>().ToListAsync();
    }

    public async Task<Provider?> GetByNameAsync(string name)
    {
        return await Context.Set<Provider>()
            .FirstOrDefaultAsync(p => p.Name == name);
    }
}
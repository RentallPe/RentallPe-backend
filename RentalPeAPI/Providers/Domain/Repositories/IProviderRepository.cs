using RentalPeAPI.Shared.Domain.Repositories;
using RentalPeAPI.providers.Domain.Model.Aggregates;

namespace RentalPeAPI.providers.Domain.Repositories;

public interface IProviderRepository : IBaseRepository<Provider>
{
    Task AddAsync(Provider provider);
    Task UpdateAsync(Provider provider);
    Task DeleteAsync(Provider provider);

    Task<Provider?> FindByIdAsync(int id);
    Task<IEnumerable<Provider>> FindAllAsync();
}
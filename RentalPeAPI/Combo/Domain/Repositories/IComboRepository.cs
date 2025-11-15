using RentalPeAPI.Combo.Domain.Aggregates.Entities;

namespace RentalPeAPI.Combo.Domain.Repositories;

public interface IComboRepository
{
    Task AddAsync(Aggregates.Entities.Combo combo);
    Task<Aggregates.Entities.Combo?> FindByIdAsync(int id);
    Task<IEnumerable<Aggregates.Entities.Combo>> ListAsync(int? providerId = null);
    void Remove(Aggregates.Entities.Combo combo);
}
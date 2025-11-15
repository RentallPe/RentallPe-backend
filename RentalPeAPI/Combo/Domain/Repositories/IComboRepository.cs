using RentalPeAPI.Combo.Domain.Aggregates.Entities;

namespace RentalPeAPI.Combo.Domain.Repositories;

public interface IComboRepository
{
    Task AddAsync(Aggregates.Entities.Combo combo);
    Task<Aggregates.Entities.Combo?> FindByIdAsync(int id);
    Task<IEnumerable<Aggregates.Entities.Combo>> ListAsync(Guid? providerId = null); // EDT 2025-11-15 Braulio
    void Remove(Aggregates.Entities.Combo combo);
}
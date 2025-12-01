using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profile.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Model.Aggregates.Profile>
{
    Task<Model.Aggregates.Profile?> FindByUserIdAsync(long userId);
    Task<Model.Aggregates.Profile?> FindByEmailAsync(string email);
}
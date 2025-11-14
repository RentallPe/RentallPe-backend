using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profile.Domain.Repositories;

/// <summary>
/// Repositorio de perfiles.
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByUserIdAsync(long userId);
}
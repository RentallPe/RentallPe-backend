using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profile.Domain.Repositories;

public interface IPreferenceSetRepository : IBaseRepository<PreferenceSet>
{
    Task<PreferenceSet?> FindByUserIdAsync(long userId);
    Task<IEnumerable<PreferenceSet>> FindByLanguageAsync(LanguageCode language);
    Task<IEnumerable<PreferenceSet>> FindByThemeAsync(ThemeMode theme);
}
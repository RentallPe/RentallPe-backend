using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Enums;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;

public class PreferenceSetRepository(AppDbContext context)
    : BaseRepository<PreferenceSet>(context), IPreferenceSetRepository
{
    public async Task<PreferenceSet?> FindByUserIdAsync(long userId)
        => await Context.Set<PreferenceSet>()
            .FirstOrDefaultAsync(p => p.UserId.Value == userId);

    public async Task<IEnumerable<PreferenceSet>> FindByLanguageAsync(LanguageCode language)
        => await Context.Set<PreferenceSet>()
            .Where(p => p.Language == language)
            .ToListAsync();

    public async Task<IEnumerable<PreferenceSet>> FindByThemeAsync(ThemeMode theme)
        => await Context.Set<PreferenceSet>()
            .Where(p => p.Theme == theme)
            .ToListAsync();
}
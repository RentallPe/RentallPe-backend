using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;

namespace RentalPeAPI.Profile.Application.Internal.QueryServices;

public class PreferenceSetQueryService(IPreferenceSetRepository preferenceRepository) : IPreferenceSetQueryService
{
    public Task<PreferenceSet?> Handle(GetPreferenceSetByIdQuery query)
        => preferenceRepository.FindByIdAsync(query.Id);

    public Task<PreferenceSet?> Handle(GetPreferenceSetByUserIdQuery query)
        => preferenceRepository.FindByUserIdAsync(query.UserId);

    public Task<IEnumerable<PreferenceSet>> Handle(GetPreferenceSetsByLanguageQuery query)
        => preferenceRepository.FindByLanguageAsync(query.Language);

    public Task<IEnumerable<PreferenceSet>> Handle(GetPreferenceSetsByThemeQuery query)
        => preferenceRepository.FindByThemeAsync(query.Theme);
}
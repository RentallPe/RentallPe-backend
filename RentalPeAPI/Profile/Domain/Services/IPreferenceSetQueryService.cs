using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Queries;

namespace RentalPeAPI.Profile.Domain.Services;

public interface IPreferenceSetQueryService
{
    Task<PreferenceSet?> Handle(GetPreferenceSetByIdQuery query);
    Task<PreferenceSet?> Handle(GetPreferenceSetByUserIdQuery query);
    Task<IEnumerable<PreferenceSet>> Handle(GetPreferenceSetsByLanguageQuery query);
    Task<IEnumerable<PreferenceSet>> Handle(GetPreferenceSetsByThemeQuery query);
}
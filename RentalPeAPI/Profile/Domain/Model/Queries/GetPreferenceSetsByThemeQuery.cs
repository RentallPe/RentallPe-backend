using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Domain.Model.Queries;

public record GetPreferenceSetsByThemeQuery(ThemeMode Theme);
using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdatePreferenceThemeCommand(int PreferenceSetId, ThemeMode Theme);
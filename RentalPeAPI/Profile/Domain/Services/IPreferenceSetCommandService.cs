using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Commands;

namespace RentalPeAPI.Profile.Domain.Services;

public interface IPreferenceSetCommandService
{
    Task<PreferenceSet?> Handle(CreatePreferenceSetCommand command);
    Task<PreferenceSet?> Handle(UpdatePreferenceLanguageCommand command);
    Task<PreferenceSet?> Handle(UpdatePreferenceThemeCommand command);
    Task<PreferenceSet?> Handle(UpdatePreferenceTimeZoneCommand command);
    Task<PreferenceSet?> Handle(SetPreferenceNotificationsCommand command);
    Task<PreferenceSet?> Handle(SetPreferencePrivacyCommand command);
    Task<PreferenceSet?> Handle(SetQuietHoursCommand command);
    Task<PreferenceSet?> Handle(ClearQuietHoursCommand command);
    Task<PreferenceSet?> Handle(AddFavoriteCommand command);
    Task<PreferenceSet?> Handle(RemoveFavoriteCommand command);
}
using System.ComponentModel.DataAnnotations;
using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record CreatePreferenceSetResource(
    [Required] long UserId,
    [Required] LanguageCode Language,
    [Required] ThemeMode Theme,
    [Required, MaxLength(100)] string TimeZone,
    [Required] NotificationPrefsResource Notifications,
    [Required] PrivacySettingsResource Privacy,
    QuietHoursResource? QuietHours
);
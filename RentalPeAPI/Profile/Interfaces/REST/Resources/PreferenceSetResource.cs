using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record PreferenceSetResource(
    int Id,
    long UserId,
    LanguageCode Language,
    ThemeMode Theme,
    string TimeZone,
    NotificationPrefsResource Notifications,
    PrivacySettingsResource Privacy,
    QuietHoursResource? QuietHours,
    DateTimeOffset? CreatedAt
);
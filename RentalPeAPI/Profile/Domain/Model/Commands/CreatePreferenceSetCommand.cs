using RentalPeAPI.Profile.Domain.Model.Enums;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record CreatePreferenceSetCommand(
    UserId UserId,
    LanguageCode Language,
    ThemeMode Theme,
    string TimeZone,
    NotificationPrefs Notifications,
    PrivacySettings Privacy,
    QuietHours? QuietHours = null);
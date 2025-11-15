using System.Globalization;
using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class UpdatePreferenceCommandsFromResourceAssembler
{
    public static UpdatePreferenceLanguageCommand From(int prefSetId, UpdatePreferenceLanguageResource r)
        => new(prefSetId, r.Language);

    public static UpdatePreferenceThemeCommand From(int prefSetId, UpdatePreferenceThemeResource r)
        => new(prefSetId, r.Theme);

    public static UpdatePreferenceTimeZoneCommand From(int prefSetId, UpdatePreferenceTimeZoneResource r)
        => new(prefSetId, r.TimeZone);

    public static SetPreferenceNotificationsCommand From(int prefSetId, SetPreferenceNotificationsResource r)
        => new(prefSetId, new NotificationPrefs(r.Notifications.Email, r.Notifications.Sms, r.Notifications.Push, r.Notifications.InApp));

    public static SetPreferencePrivacyCommand From(int prefSetId, SetPreferencePrivacyResource r)
        => new(prefSetId, new PrivacySettings(r.Privacy.IsProfilePublic, r.Privacy.ShowEmail, r.Privacy.ShowPhone, r.Privacy.ShareActivity));

    public static SetQuietHoursCommand From(int prefSetId, SetQuietHoursResource r)
        => new(prefSetId, new QuietHours(
            TimeOnly.ParseExact(r.QuietHours.Start, "HH:mm", CultureInfo.InvariantCulture),
            TimeOnly.ParseExact(r.QuietHours.End,   "HH:mm", CultureInfo.InvariantCulture)));
}
using System.Globalization;
using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class CreatePreferenceSetCommandFromResourceAssembler
{
    public static CreatePreferenceSetCommand ToCommandFromResource(CreatePreferenceSetResource r)
        => new(
            new UserId(r.UserId),
            r.Language,
            r.Theme,
            r.TimeZone,
            new Domain.Model.ValueObjects.NotificationPrefs(
                r.Notifications.Email,
                r.Notifications.Sms,
                r.Notifications.Push,
                r.Notifications.InApp),
            new Domain.Model.ValueObjects.PrivacySettings(
                r.Privacy.IsProfilePublic,
                r.Privacy.ShowEmail,
                r.Privacy.ShowPhone,
                r.Privacy.ShareActivity),
            r.QuietHours is null
                ? null
                : new QuietHours(
                    TimeOnly.ParseExact(r.QuietHours.Start, "HH:mm", CultureInfo.InvariantCulture),
                    TimeOnly.ParseExact(r.QuietHours.End,   "HH:mm", CultureInfo.InvariantCulture))
        );
}
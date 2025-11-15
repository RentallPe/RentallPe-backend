using System.Globalization;
using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class PreferenceSetResourceFromEntityAssembler
{
    private const string TimeFmt = "HH\\:mm";

    public static PreferenceSetResource ToResourceFromEntity(PreferenceSet entity)
        => new(
            entity.Id,
            entity.UserId.Value,
            entity.Language,
            entity.Theme,
            entity.TimeZone,
            new NotificationPrefsResource(
                entity.Notifications.Email,
                entity.Notifications.Sms,
                entity.Notifications.Push,
                entity.Notifications.InApp),
            new PrivacySettingsResource(
                entity.Privacy.IsProfilePublic,
                entity.Privacy.ShowEmail,
                entity.Privacy.ShowPhone,
                entity.Privacy.ShareActivity),
            entity.QuietHours is null
                ? null
                : new QuietHoursResource(
                    entity.QuietHours.Start.ToString(TimeFmt, CultureInfo.InvariantCulture),
                    entity.QuietHours.End.ToString(TimeFmt, CultureInfo.InvariantCulture)),
            entity.CreatedDate
        );
}
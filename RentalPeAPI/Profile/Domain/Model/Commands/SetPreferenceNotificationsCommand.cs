using RentalPeAPI.Profile.Domain.Model.ValueObjects;

namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record SetPreferenceNotificationsCommand(int PreferenceSetId, NotificationPrefs Notifications);
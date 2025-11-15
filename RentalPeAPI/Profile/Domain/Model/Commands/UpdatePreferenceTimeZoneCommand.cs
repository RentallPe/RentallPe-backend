namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record UpdatePreferenceTimeZoneCommand(int PreferenceSetId, string TimeZone);
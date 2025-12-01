namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record RemoveFavoriteCommand(int PreferenceSetId, long RemodelingId);
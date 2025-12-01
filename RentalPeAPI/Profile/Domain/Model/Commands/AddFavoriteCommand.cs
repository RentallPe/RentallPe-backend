namespace RentalPeAPI.Profile.Domain.Model.Commands;

public sealed record AddFavoriteCommand(int PreferenceSetId, long RemodelingId);
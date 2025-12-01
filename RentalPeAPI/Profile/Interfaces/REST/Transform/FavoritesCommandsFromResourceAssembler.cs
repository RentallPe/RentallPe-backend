using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class FavoritesCommandsFromResourceAssembler
{
    public static AddFavoriteCommand From(int prefSetId, AddFavoriteResource r)
        => new(prefSetId, r.RemodelingId);

    public static RemoveFavoriteCommand From(int prefSetId, RemoveFavoriteResource r)
        => new(prefSetId, r.RemodelingId);
}
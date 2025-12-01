using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Interfaces.REST.Resources;

namespace RentalPeAPI.Combo.Interfaces.REST.Transform;

public static class ComboCommandAssembler
{
    public static CreateComboCommand ToCommand(CreateComboResource r)
        => new(
            r.Name,
            r.Description,
            r.Price,
            r.InstallDays,
            r.Image,
            r.ProviderId
        );

    public static UpdateComboCommand ToCommand(int id, UpdateComboResource r)
        => new(
            id,
            r.Name,
            r.Description,
            r.Price,
            r.InstallDays,
            r.Image,
            r.ProviderId
        );
}
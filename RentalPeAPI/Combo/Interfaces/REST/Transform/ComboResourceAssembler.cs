using RentalPeAPI.Combo.Application.Internal.Dtos;
using RentalPeAPI.Combo.Interfaces.REST.Resources;

namespace RentalPeAPI.Combo.Interfaces.REST.Transform;

public static class ComboResourceAssembler
{
    public static ComboResource ToResource(ComboDto dto)
    {
        return new ComboResource
        {
            Id = dto.Id,
            ProviderId = dto.ProviderId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            InstallDays = dto.InstallDays,
            Image = dto.Image
        };
    }
}
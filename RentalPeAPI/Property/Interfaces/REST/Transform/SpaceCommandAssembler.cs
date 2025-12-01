using RentalPeAPI.Property.Application.Internal.CommandServices;
using RentalPeAPI.Property.Interfaces.Rest.Resources;

namespace RentalPeAPI.Property.Interfaces.Rest.Transform
{
    public static class SpaceCommandAssembler
    {
        public static CreateSpaceCommand ToCommand(CreateSpaceResource resource)
            => new(
                resource.Name,
                resource.Description,
                resource.PricePerHour,
                resource.Type,
                resource.Address,
                resource.OwnerId,
                resource.Services ?? new List<string>(),
                resource.Status,    // ⚠️ status va antes que areaM2
                resource.AreaM2
            );

        public static UpdateSpaceCommand ToCommand(long id, UpdateSpaceResource resource)
            => new(
                id,
                resource.Name,
                resource.Description,
                resource.PricePerHour,
                resource.Type,
                resource.Address,    // equivale a Location
                resource.Services ?? new List<string>(),
                resource.Status,     // ⚠️ status antes de areaM2
                resource.AreaM2
            );
    }
}
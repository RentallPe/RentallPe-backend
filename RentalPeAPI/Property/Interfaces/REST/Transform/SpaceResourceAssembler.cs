using RentalPeAPI.Property.Application.Internal.Dtos;
using RentalPeAPI.Property.Interfaces.Rest.Resources;

namespace RentalPeAPI.Property.Interfaces.Rest.Transform
{
    public static class SpaceResourceAssembler
    {
        public static SpaceResource ToResource(SpaceDto dto)
        {
            if (dto == null) return null!;

            return new SpaceResource
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                PricePerHour = dto.PricePerHour,
                Type = dto.Type,
                Address = dto.Location,
                OwnerId = dto.OwnerId,
                Services = dto.Services?.Select(s => s.Name).ToList() ?? new List<string>(),
                AreaM2 = dto.AreaM2,
                Status = dto.Status,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}
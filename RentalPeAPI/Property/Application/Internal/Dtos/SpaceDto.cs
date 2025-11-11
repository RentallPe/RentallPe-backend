using RentalPeAPI.Property.Domain.Aggregates;

namespace RentalPeAPI.Property.Application.Internal.Dtos;

public class SpaceDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public long OwnerId { get; set; }
    public List<ServiceDto> Services { get; set; } = new();
    public string Status { get; set; } = "available";
    public decimal AreaM2 { get; set; }
    public DateTime CreatedAt { get; set; }

    public static SpaceDto FromDomain(Space space)
    {
        return new SpaceDto
        {
            Id = space.Id,
            Name = space.Name,
            Description = space.Description,
            PricePerHour = space.PricePerHour,
            Type = space.Type.ToString(),
            Location = space.Location.ToString(), // si tu Location tiene un override de ToString(), o usa space.Location.Address
            OwnerId = space.OwnerId.Value,
            Status = space.Status,
            AreaM2 = space.AreaM2,
            CreatedAt = space.CreatedAt,
            Services = space.Services.Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList()
        };
    }
}
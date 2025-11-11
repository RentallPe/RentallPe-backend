using RentalPeAPI.Property.Domain.Aggregates.Entities;
using RentalPeAPI.Property.Domain.Aggregates.Enums;
using RentalPeAPI.Property.Domain.Aggregates.ValueObjects;

namespace RentalPeAPI.Property.Domain.Aggregates;

public class Space
{
    public long Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal PricePerHour { get; private set; }
    public SpaceType Type { get; private set; }
    public Location Location { get; private set; } = default!;
    public OwnerId OwnerId { get; private set; }
    public List<Service> Services { get; private set; } = new();

    // Campos adicionales para alinear con "Property"
    public string Status { get; private set; } = "available";
    public decimal AreaM2 { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    protected Space() { }

    public Space(
        string name,
        string description,
        decimal pricePerHour,
        string type,
        string location,
        long ownerId,
        IEnumerable<string> services,
        decimal areaM2 = 0,
        string status = "available"
    )
    {
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        Type = Enum.TryParse<SpaceType>(type, true, out var parsedType)
            ? parsedType
            : SpaceType.Other;
        Location = new Location(location);
        OwnerId = new OwnerId(ownerId);
        Services = services.Select(s => new Service(s)).ToList();
        AreaM2 = areaM2;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string description,
        decimal pricePerHour,
        string type,
        string location,
        IEnumerable<string> services,
        decimal areaM2,
        string status
    )
    {
        Name = name;
        Description = description;
        PricePerHour = pricePerHour;
        Type = Enum.TryParse<SpaceType>(type, true, out var parsedType)
            ? parsedType
            : SpaceType.Other;
        Location = new Location(location);
        AreaM2 = areaM2;
        Status = status;

        Services.Clear();
        Services.AddRange(services.Select(s => new Service(s)));
    }
}

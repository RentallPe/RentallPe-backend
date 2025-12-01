namespace RentalPeAPI.Property.Application.Internal.CommandServices
{
    public class CreateSpaceCommand
    {
        public string Name { get; }
        public string Description { get; }
        public decimal PricePerHour { get; }
        public string Type { get; }
        public string Location { get; }
        public long OwnerId { get; }
        public string Status { get; set; }
        public decimal AreaM2 { get; set; }

        public IEnumerable<string> Services { get; }

        public CreateSpaceCommand(string name, string description, decimal pricePerHour, string type, string location, long ownerId, IEnumerable<string> services, string status, decimal areaM2)
        {
            Name = name;
            Description = description;
            PricePerHour = pricePerHour;
            Type = type;
            Location = location;
            OwnerId = ownerId;
            Services = services;
            Status = status;
            AreaM2 = areaM2;
        }
    }
}
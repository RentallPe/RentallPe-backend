namespace RentalPeAPI.Property.Application.Internal.CommandServices
{
    public class UpdateSpaceCommand
    {
        public long Id { get; }

        public string Name { get; }
        public string Description { get; }
        public decimal PricePerHour { get; }
        public string Type { get; }
        public string Location { get; }
        public string Status { get; set; }
        public decimal AreaM2 { get; set; }
        public IEnumerable<string> Services { get; }
        

        public UpdateSpaceCommand(long id, string name, string description, decimal pricePerHour, string type, string location, IEnumerable<string> services, string status, decimal areaM2)
        {
            Id = id;
            Name = name;
            Description = description;
            PricePerHour = pricePerHour;
            Type = type;
            Location = location;
            Services = services;
            Status = status;
            AreaM2 = areaM2;
        }
    }
}
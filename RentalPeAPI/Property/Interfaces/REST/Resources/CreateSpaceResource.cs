namespace RentalPeAPI.Property.Interfaces.Rest.Resources
{
    public class CreateSpaceResource
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal PricePerHour { get; set; }
        public string Type { get; set; } = "";
        public string Address { get; set; } = "";
        public long OwnerId { get; set; }
        public List<string> Services { get; set; } = new();
        
        // 👇 Nuevos campos alineados con tu entidad Space
        public decimal AreaM2 { get; set; }
        public string Status { get; set; } = "available";
    }
}
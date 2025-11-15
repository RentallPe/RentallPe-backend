namespace RentalPeAPI.Combo.Interfaces.REST.Resources
{
    public class UpdateComboResource
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int InstallDays { get; set; }
        public string Image { get; set; } = "";
        public int ProviderId { get; set; }
    }
}
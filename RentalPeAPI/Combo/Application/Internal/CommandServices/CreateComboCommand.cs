namespace RentalPeAPI.Combo.Application.Internal.CommandServices;

public class CreateComboCommand
{
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int InstallDays { get; }
    public string Image { get; }
    public Guid ProviderId { get; } // EDT 2025-11-15 Braulio
    public string PlanType { get; }


    public CreateComboCommand(string name, string description, decimal price, int installDays, string image, Guid providerId,string planType) // EDT 2025-11-15 Braulio
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;
        Image = image;
        ProviderId = providerId; // EDT 2025-11-15 Braulio
        PlanType = planType;


    }
}
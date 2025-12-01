namespace RentalPeAPI.Combo.Application.Internal.CommandServices;

public class UpdateComboCommand
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int InstallDays { get; }
    public string Image { get; }
    public Guid ProviderId { get; } // EDT 2025-11-15 Braulio


    public UpdateComboCommand(int id, string name, string description, decimal price, int installDays, string image, Guid providerId) // EDT 2025-11-15 Braulio
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;
        Image = image;
        ProviderId = providerId; // EDT 2025-11-15 Braulio
    }
}
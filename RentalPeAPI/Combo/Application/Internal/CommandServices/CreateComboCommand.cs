namespace RentalPeAPI.Combo.Application.Internal.CommandServices;

public class CreateComboCommand
{
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int InstallDays { get; }
    public string Image { get; }
    public int ProviderId { get; }

    public CreateComboCommand(string name, string description, decimal price, int installDays, string image, int providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;
        Image = image;
        ProviderId = providerId;
    }
}
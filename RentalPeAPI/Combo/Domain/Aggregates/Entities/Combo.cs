namespace RentalPeAPI.Combo.Domain.Aggregates.Entities;
using RentalPeAPI.Combo.Domain.Aggregates.ValueObjects;

public class Combo
{
    public int Id { get; set; }
    public Guid ProviderId { get; set; } // NUE 2025-11-15 Braulio


    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }
    public int InstallDays { get; private set; }


    public string Image { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    protected Combo() { }

    public Combo(string name, string description, decimal price, int installDays, string image, Guid providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;

        Image = image; // ahora es string

        ProviderId = providerId;
    }

    public void Update(string name, string description, decimal price, int installDays, string image, Guid providerId)
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;

        Image = image; // ahora es string

        ProviderId = providerId;
    }
}
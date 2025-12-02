namespace RentalPeAPI.Combo.Domain.Aggregates.Entities;
using RentalPeAPI.Combo.Domain.Aggregates.ValueObjects;

public class Combo
{
    public int Id { get; set; }
    public Guid ProviderId { get; private set; } // NUE 2025-11-15 Braulio

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }
    public int InstallDays { get; private set; }

    public string Image { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string PlanType { get; private set; } = "basic"; // 👈 setter privado

    protected Combo() { }

    public Combo(string name, string description, decimal price, int installDays, string image, Guid providerId, string planType)
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;
        Image = image;
        ProviderId = providerId;
        PlanType = planType; // 👈 ahora se asigna desde el constructor
    }

    public void Update(string name, string description, decimal price, int installDays, string image, Guid providerId, string planType)
    {
        Name = name;
        Description = description;
        Price = price;
        InstallDays = installDays;
        Image = image;
        ProviderId = providerId;
        PlanType = planType; // 👈 ahora se actualiza desde el método Update
    }
}
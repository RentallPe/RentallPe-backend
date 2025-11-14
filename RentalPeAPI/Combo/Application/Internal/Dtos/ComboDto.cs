namespace RentalPeAPI.Combo.Application.Internal.Dtos;
using RentalPeAPI.Combo.Domain.Aggregates.Entities;

public class ComboDto
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public int InstallDays { get; set; }
    public string Image { get; set; } = string.Empty;

    public static ComboDto FromDomain(Combo combo)
    {
        return new ComboDto
        {
            Id = combo.Id,
            ProviderId = combo.ProviderId,
            Name = combo.Name,
            Description = combo.Description,
            Price = combo.Price,             // 👈 VO
            InstallDays = combo.InstallDays, // 👈 VO
            Image = combo.Image               // 👈 VO
        };
    }
}
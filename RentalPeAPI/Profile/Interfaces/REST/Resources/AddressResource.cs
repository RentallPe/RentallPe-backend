using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record AddressResource(
    [Required, MaxLength(200)] string Line1,
    [MaxLength(200)] string? Line2,
    [MaxLength(100)] string? District,
    [Required, MaxLength(100)] string City,
    [MaxLength(100)] string? State,
    [MaxLength(32)] string? PostalCode,
    [Required, MaxLength(100)] string Country
);
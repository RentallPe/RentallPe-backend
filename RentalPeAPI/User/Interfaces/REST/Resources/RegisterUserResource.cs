using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.User.Interfaces.REST.Resources;

public record RegisterUserResource(
    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(8)]
    string Password,

    [Required]
    string FullName,
    
    string? Phone = null, // NUE 2025-11-15 Braulio
    string Role = "customer", // NUE 2025-11-15 Braulio
    Guid? ProviderId = null, // NUE 2025-11-15 Braulio
    string? Photo = null // NUE 2025-11-15 Braulio

);
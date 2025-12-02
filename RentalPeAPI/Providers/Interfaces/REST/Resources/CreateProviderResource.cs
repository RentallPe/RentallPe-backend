using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.providers.Interfaces.REST.Resources;

public record CreateProviderResource(
    [Required] string Name,
    [Required, EmailAddress] string ContactEmail);
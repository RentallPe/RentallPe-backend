using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record UpdateProfileNameResource([Required, MaxLength(200)] string FullName);
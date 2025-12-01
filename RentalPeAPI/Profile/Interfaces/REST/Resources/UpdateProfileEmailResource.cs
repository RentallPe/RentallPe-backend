using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record UpdateProfileEmailResource([Required, EmailAddress, MaxLength(320)] string Email);
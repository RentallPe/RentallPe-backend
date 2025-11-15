using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record UpdateProfileBioResource([MaxLength(1000)] string? Bio);
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record CreateProfileResource(
    [Required] long UserId,
    [Required, MaxLength(200)] string FullName,
    [Required, EmailAddress, MaxLength(320)] string PrimaryEmail,
    [Required] AvatarResource Avatar,
    [MaxLength(1000)] string? Bio,
    PhoneResource? PrimaryPhone,
    AddressResource? PrimaryAddress
);
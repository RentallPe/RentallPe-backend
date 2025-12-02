using RentalPeAPI.Profiles.Domain.Model.Enums;

namespace RentalPeAPI.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Photo,
    ProfileRole Role);
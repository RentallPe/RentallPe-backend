namespace RentalPeAPI.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Photo,
    string Role);
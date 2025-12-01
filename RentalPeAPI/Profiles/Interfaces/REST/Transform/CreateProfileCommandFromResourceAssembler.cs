using RentalPeAPI.Profiles.Domain.Model.Commands;
using RentalPeAPI.Profiles.Domain.Model.Enums;
using RentalPeAPI.Profiles.Interfaces.REST.Resources;

namespace RentalPeAPI.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        var role = ParseRole(resource.Role);

        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Password,
            resource.Phone,
            resource.Photo,
            role);
    }

    private static ProfileRole ParseRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return ProfileRole.Customer;

        return role.Trim().ToLowerInvariant() switch
        {
            "provider" => ProfileRole.Provider,
            _ => ProfileRole.Customer
        };
    }
}
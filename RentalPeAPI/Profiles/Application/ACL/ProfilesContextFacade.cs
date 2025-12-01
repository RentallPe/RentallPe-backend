using RentalPeAPI.Profiles.Domain.Model.Commands;
using RentalPeAPI.Profiles.Domain.Model.Enums;
using RentalPeAPI.Profiles.Domain.Model.Queries;
using RentalPeAPI.Profiles.Domain.Model.ValueObjects;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Profiles.Interfaces.ACL;

namespace RentalPeAPI.Profiles.Application.ACL;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService
) : IProfilesContextFacade
{
    public async Task<int> CreateProfile(
        string firstName,
        string lastName,
        string email,
        string password,
        string phone,
        string photo,
        string role)
    {
        var roleEnum = ParseRole(role);

        var command = new CreateProfileCommand(
            firstName,
            lastName,
            email,
            password,
            phone,
            photo,
            roleEnum);

        var profile = await profileCommandService.Handle(command);
        return profile?.Id ?? 0;
    }

    public async Task<int> FetchProfileIdByEmail(string email)
    {
        var query = new GetProfileByEmailQuery(new EmailAddress(email));
        var profile = await profileQueryService.Handle(query);
        return profile?.Id ?? 0;
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
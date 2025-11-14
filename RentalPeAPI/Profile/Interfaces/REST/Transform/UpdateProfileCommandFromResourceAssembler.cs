using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.ValueObjects;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Converts UpdateProfileResource into UpdateProfileCommand.
/// </summary>
public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(
        UpdateProfileResource resource,
        int profileId)
    {
        var address = resource.PrimaryAddress is null
            ? null
            : new Address(
                resource.PrimaryAddress.Line1,
                resource.PrimaryAddress.Line2,
                resource.PrimaryAddress.District,
                resource.PrimaryAddress.City,
                resource.PrimaryAddress.State,
                resource.PrimaryAddress.PostalCode,
                resource.PrimaryAddress.Country);

        return new UpdateProfileCommand(
            profileId,
            resource.FullName,
            resource.Bio,
            resource.AvatarUrl,
            resource.PrimaryEmail,
            resource.PrimaryPhone,
            address);
    }
}
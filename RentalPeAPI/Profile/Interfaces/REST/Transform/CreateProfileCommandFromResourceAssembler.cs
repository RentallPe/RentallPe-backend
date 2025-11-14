using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Commands;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Converts CreateProfileResource into CreateProfileCommand.
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.UserId,
            resource.FullName,
            resource.PrimaryEmail,
            resource.Country,
            resource.Department,
            resource.Bio,
            resource.AvatarUrl);
    }
}
namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Resource used to create a new profile.
/// </summary>
/// <param name="UserId">IAM user identifier.</param>
/// <param name="FullName">Full name of the user.</param>
/// <param name="PrimaryEmail">Primary email of the user.</param>
/// <param name="Country">Country where the user lives.</param>
/// <param name="Department">Department or region.</param>
/// <param name="Bio">Optional biography.</param>
/// <param name="AvatarUrl">Optional avatar url.</param>
public record CreateProfileResource(
    int UserId,
    string FullName,
    string PrimaryEmail,
    string Country,
    string Department,
    string? Bio,
    string? AvatarUrl);
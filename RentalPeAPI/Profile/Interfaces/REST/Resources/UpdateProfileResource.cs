namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Resource used to update an existing profile.
/// </summary>
/// <param name="FullName">New full name.</param>
/// <param name="Bio">New biography.</param>
/// <param name="AvatarUrl">New avatar url.</param>
/// <param name="PrimaryEmail">New primary email.</param>
/// <param name="PrimaryPhone">New primary phone.</param>
/// <param name="PrimaryAddress">New primary address.</param>
public record UpdateProfileResource(
    string FullName,
    string? Bio,
    string? AvatarUrl,
    string PrimaryEmail,
    string? PrimaryPhone,
    AddressResource? PrimaryAddress);
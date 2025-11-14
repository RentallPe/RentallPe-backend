namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

/// <summary>
///     Profile resource for REST API.
/// </summary>
/// <param name="Id">Profile identifier.</param>
/// <param name="UserId">IAM user identifier.</param>
/// <param name="FullName">Full name of the user.</param>
/// <param name="Country">Country where the user lives.</param>
/// <param name="Department">Department/region of the user.</param>
/// <param name="Bio">Short biography.</param>
/// <param name="AvatarUrl">Avatar image url.</param>
/// <param name="PrimaryEmail">Primary email address.</param>
/// <param name="PrimaryPhone">Primary phone number.</param>
/// <param name="PrimaryAddress">Primary address information.</param>
/// <param name="PaymentMethods">List of payment methods associated to the profile.</param>
public record ProfileResource(
    int Id,
    int UserId,
    string FullName,
    string Country,
    string Department,
    string? Bio,
    string? AvatarUrl,
    string PrimaryEmail,
    string? PrimaryPhone,
    AddressResource? PrimaryAddress,
    IEnumerable<string> PaymentMethods);
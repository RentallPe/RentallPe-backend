namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record ProfileResource(
    int Id,
    long UserId,
    string FullName,
    string? Bio,
    AvatarResource Avatar,
    string PrimaryEmail,
    PhoneResource? PrimaryPhone,
    AddressResource? PrimaryAddress,
    DateTimeOffset? CreatedAt
);
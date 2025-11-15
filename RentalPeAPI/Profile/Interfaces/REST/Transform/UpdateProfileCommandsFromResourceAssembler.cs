using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class UpdateProfileCommandsFromResourceAssembler
{
    public static UpdateProfileNameCommand From(int profileId, UpdateProfileNameResource r)
        => new(profileId, r.FullName);

    public static UpdateProfileBioCommand From(int profileId, UpdateProfileBioResource r)
        => new(profileId, r.Bio);

    public static UpdateProfileEmailCommand From(int profileId, UpdateProfileEmailResource r)
        => new(profileId, r.Email);

    public static UpdateProfilePhoneCommand From(int profileId, UpdateProfilePhoneResource r)
        => new(profileId, r.Phone is null ? null : new Phone(r.Phone.Number));

    public static UpdateProfileAddressCommand From(int profileId, UpdateProfileAddressResource r)
        => new(profileId, r.Address is null
            ? null
            : new Address(
                r.Address.Line1,
                r.Address.City,
                r.Address.Country,
                r.Address.Line2,
                r.Address.District,
                r.Address.State,
                r.Address.PostalCode));

    public static UpdateProfileAvatarCommand From(int profileId, UpdateProfileAvatarResource r)
        => new(profileId, new Avatar(r.Avatar.Url));
}
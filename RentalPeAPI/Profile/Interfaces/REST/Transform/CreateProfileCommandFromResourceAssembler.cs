using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Model.ValueObjects;
using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource r)
        => new(
            new UserId(r.UserId),
            r.FullName,
            r.PrimaryEmail,
            new Avatar(r.Avatar.Url),
            r.Bio,
            r.PrimaryPhone is null ? null : new Phone(r.PrimaryPhone.Number),
            r.PrimaryAddress is null
                ? null
                : new Address(
                    r.PrimaryAddress.Line1,
                    r.PrimaryAddress.City,
                    r.PrimaryAddress.Country,
                    r.PrimaryAddress.Line2,
                    r.PrimaryAddress.District,
                    r.PrimaryAddress.State,
                    r.PrimaryAddress.PostalCode)
        );
}
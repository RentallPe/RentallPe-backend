using RentalPeAPI.Profile.Interfaces.REST.Resources;

namespace RentalPeAPI.Profile.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Domain.Model.Aggregates.Profile entity)
        => new(
            entity.Id,
            entity.UserId.Value,
            entity.FullName,
            entity.Bio,
            new AvatarResource(entity.Avatar.Url),
            entity.PrimaryEmail,
            entity.PrimaryPhone is null ? null : new PhoneResource(entity.PrimaryPhone.Number),
            entity.PrimaryAddress is null
                ? null
                : new AddressResource(
                    entity.PrimaryAddress.Line1,
                    entity.PrimaryAddress.Line2,
                    entity.PrimaryAddress.District,
                    entity.PrimaryAddress.City,
                    entity.PrimaryAddress.State,
                    entity.PrimaryAddress.PostalCode,
                    entity.PrimaryAddress.Country),
            entity.CreatedDate
        );
}
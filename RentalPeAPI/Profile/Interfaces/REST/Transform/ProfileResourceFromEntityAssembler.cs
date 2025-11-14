using ACME.LearningCenterPlatform.API.Profiles.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Resources;

namespace ACME.LearningCenterPlatform.API.Profiles.Interfaces.REST.Transform;

/// <summary>
///     Assembler to convert Profile entity to ProfileResource.
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        AddressResource? addressResource = null;

        if (entity.PrimaryAddress is not null)
        {
            addressResource = new AddressResource(
                entity.PrimaryAddress.Line1,
                entity.PrimaryAddress.Line2,
                entity.PrimaryAddress.District,
                entity.PrimaryAddress.City,
                entity.PrimaryAddress.State,
                entity.PrimaryAddress.PostalCode,
                entity.PrimaryAddress.Country);
        }

        return new ProfileResource(
            entity.Id,
            entity.UserId.Value,
            entity.FullName,
            entity.Country,
            entity.Department,
            entity.Bio,
            entity.AvatarUrl,
            entity.PrimaryEmail,
            entity.PrimaryPhone,
            addressResource,
            entity.PaymentMethods);
    }
}
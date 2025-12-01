using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Interfaces.REST.Resources;

namespace RentalPeAPI.Profiles.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        var paymentMethods = entity.PaymentMethods
            .Select(pm => new PaymentMethodResource(
                pm.Id,
                pm.Type,
                pm.Number,
                pm.Expiry,
                pm.Cvv))
            .ToList();

        var role = entity.Role.ToString().ToLowerInvariant();

        return new ProfileResource(
            entity.Id,
            entity.FullName,
            entity.EmailAddress,
            entity.Password,
            entity.PhoneNumber,
            entity.CreatedDate,
            entity.Photo,
            role,
            paymentMethods);
    }

    public static IEnumerable<ProfileResource> ToResourceFromEntities(IEnumerable<Profile> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}
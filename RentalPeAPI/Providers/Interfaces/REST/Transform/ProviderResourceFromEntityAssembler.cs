using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Interfaces.REST.Resources;

namespace RentalPeAPI.providers.Interfaces.REST.Transform;

public static class ProviderResourceFromEntityAssembler
{
    public static ProviderResource ToResourceFromEntity(Provider entity)
        => new(
            Id: entity.Id,
            Name: entity.Name,
            ContactEmail: entity.Contact);
}
using RentalPeAPI.providers.Domain.Model.Commands;
using RentalPeAPI.providers.Interfaces.REST.Resources;

namespace RentalPeAPI.providers.Interfaces.REST.Transform;

public static class CreateProviderCommandFromResourceAssembler
{
    public static CreateProviderCommand ToCommandFromResource(CreateProviderResource resource)
        => new(
            resource.Name,
            resource.ContactEmail
        );

}
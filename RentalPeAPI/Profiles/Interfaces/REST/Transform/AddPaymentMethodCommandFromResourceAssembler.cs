using RentalPeAPI.Profiles.Domain.Model.Commands;
using RentalPeAPI.Profiles.Interfaces.REST.Resources;

namespace RentalPeAPI.Profiles.Interfaces.REST.Transform;

public static class AddPaymentMethodCommandFromResourceAssembler
{
    public static AddPaymentMethodCommand ToCommandFromResource(int profileId, AddPaymentMethodResource resource)
    {
        return new AddPaymentMethodCommand(
            profileId,
            resource.Type,
            resource.Number,
            resource.Expiry,
            resource.Cvv);
    }
}
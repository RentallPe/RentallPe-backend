using RentalPeAPI.Payment.Domain.Model.Commands.payments;
using RentalPeAPI.Payment.Domain.Model.ValueObjects;
using RentalPeAPI.Payment.Interfaces.REST.Resources;

namespace RentalPeAPI.Payment.Interfaces.REST.Transform;

public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
        => new(resource.UserId,
            new Money(resource.Money.Amount, resource.Money.Currency),
            new PaymentMethodSummary(resource.Method.Type, resource.Method.Label, resource.Method.Last4),
            resource.Reference);
}
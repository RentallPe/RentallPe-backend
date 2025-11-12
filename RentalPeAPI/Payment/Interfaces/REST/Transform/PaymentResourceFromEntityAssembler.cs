using RentalPeAPI.Payment.Interfaces.REST.Resources;

namespace RentalPeAPI.Payment.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Domain.Model.Aggregates.Payment entity)
        => new(entity.Id,
            entity.UserId,
            new MoneyResource(entity.Money.Amount, entity.Money.Currency),
            new PaymentMethodResource(entity.Method.Type, entity.Method.Label, entity.Method.Last4),
            entity.Status,
            entity.Reference,
            entity.CreatedDate);
}
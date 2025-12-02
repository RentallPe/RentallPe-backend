using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Interfaces.REST.Resources;

namespace RentalPeAPI.subscriptions.Interfaces.REST.Transform;

public static class SubscriptionResourceFromEntityAssembler
{
    public static SubscriptionResource ToResourceFromEntity(Subscription entity)
    {
        var plan   = entity.Plan.ToString().ToLowerInvariant();    // "basic" | "premium" | "enterprise"
        var status = entity.Status.ToString().ToLowerInvariant();  // "active" | "canceled" | "expired"

        return new SubscriptionResource(
            Id:         entity.Id,
            CustomerId: entity.CustomerId,
            Plan:       plan,
            Price:      entity.Price.Amount,
            Status:     status,
            StartDate:  entity.StartDate,
            EndDate:    entity.EndDate,
            CreatedAt:  entity.CreatedDate);
    }
}
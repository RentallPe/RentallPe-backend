using RentalPeAPI.subscriptions.Domain.Model.Commands;
using RentalPeAPI.subscriptions.Interfaces.REST.Resources;

namespace RentalPeAPI.subscriptions.Interfaces.REST.Transform;

public static class CreateSubscriptionCommandFromResourceAssembler
{
    public static CreateSubscriptionCommand ToCommandFromResource(CreateSubscriptionResource resource)
        => new(
            CustomerId: resource.CustomerId,
            Plan:       resource.Plan,
            Price:      resource.Price,
            StartDate:  resource.StartDate,
            EndDate:    resource.EndDate);
}
using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Domain.Model.Commands;


public sealed record CreateSubscriptionCommand(
    int CustomerId,
    SubscriptionPlan Plan,
    decimal Price,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);
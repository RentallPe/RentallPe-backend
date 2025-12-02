namespace RentalPeAPI.subscriptions.Interfaces.REST.Resources;

public record SubscriptionResource(
    int Id,
    int CustomerId,
    string Plan,
    decimal Price,
    string Status,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    DateTimeOffset? CreatedAt);
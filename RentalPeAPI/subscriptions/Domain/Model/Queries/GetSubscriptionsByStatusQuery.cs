using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Domain.Model.Queries;

public sealed record GetSubscriptionsByStatusQuery(
    SubscriptionStatus Status,
    int? CustomerId = null);
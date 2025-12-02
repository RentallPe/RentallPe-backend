using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Domain.Model.Commands;

public sealed record ChangeSubscriptionPlanCommand(
    int SubscriptionId,
    SubscriptionPlan NewPlan,
    decimal NewPrice);
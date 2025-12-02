using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Commands;

namespace RentalPeAPI.subscriptions.Domain.Services;

public interface ISubscriptionCommandService
{
    Task<Subscription?> Handle(CreateSubscriptionCommand command);
    Task<Subscription?> Handle(CancelSubscriptionCommand command);
    Task<Subscription?> Handle(ExpireSubscriptionCommand command);
    Task<Subscription?> Handle(ChangeSubscriptionPlanCommand command);
}
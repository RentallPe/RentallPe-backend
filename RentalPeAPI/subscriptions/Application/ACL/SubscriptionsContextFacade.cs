using RentalPeAPI.subscriptions.Domain.Model.Commands;
using RentalPeAPI.subscriptions.Domain.Model.Enums;
using RentalPeAPI.subscriptions.Domain.Model.Queries;
using RentalPeAPI.subscriptions.Domain.Services;
using RentalPeAPI.subscriptions.Interfaces.ACL;

namespace RentalPeAPI.subscriptions.Application.ACL;

public class SubscriptionsContextFacade(
    ISubscriptionCommandService subscriptionCommandService,
    ISubscriptionQueryService subscriptionQueryService)
    : ISubscriptionsContextFacade
{
    public async Task<int> CreateSubscription(
        int customerId,
        string plan,
        decimal price,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        if (!Enum.TryParse<SubscriptionPlan>(plan, true, out var planEnum))
            throw new ArgumentException($"Invalid subscription plan: {plan}", nameof(plan));

        var command = new CreateSubscriptionCommand(
            CustomerId: customerId,
            Plan:       planEnum,
            Price:      price,
            StartDate:  startDate,
            EndDate:    endDate);

        var subscription = await subscriptionCommandService.Handle(command);
        return subscription?.Id ?? 0;
    }

    public async Task<int> FetchActiveSubscriptionIdByCustomer(int customerId)
    {
        var query = new GetSubscriptionsByStatusQuery(
            Status:     SubscriptionStatus.ACTIVE,
            CustomerId: customerId);

        var subscriptions = await subscriptionQueryService.Handle(query);
        var active = subscriptions.FirstOrDefault();
        return active?.Id ?? 0;
    }
}
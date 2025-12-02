using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Queries;
using RentalPeAPI.subscriptions.Domain.Repositories;
using RentalPeAPI.subscriptions.Domain.Services;

namespace RentalPeAPI.subscriptions.Application.Internal.QueryServices;

public class SubscriptionQueryService(
    ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<Subscription?> Handle(GetSubscriptionByIdQuery query)
        => await subscriptionRepository.FindByIdAsync(query.SubscriptionId);

    public async Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByCustomerIdQuery query)
        => await subscriptionRepository.FindByCustomerIdAsync(query.CustomerId);

    public async Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByStatusQuery query)
        => query.CustomerId.HasValue
            ? await subscriptionRepository.FindByStatusAndCustomerIdAsync(
                query.Status, query.CustomerId.Value)
            : await subscriptionRepository.FindByStatusAsync(query.Status);
}
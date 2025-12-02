using RentalPeAPI.Shared.Domain.Repositories;
using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Domain.Repositories;

public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    Task<IEnumerable<Subscription>> FindByCustomerIdAsync(int customerId);

    Task<IEnumerable<Subscription>> FindByStatusAsync(SubscriptionStatus status);

    Task<IEnumerable<Subscription>> FindByStatusAndCustomerIdAsync(
        SubscriptionStatus status,
        int customerId);
}
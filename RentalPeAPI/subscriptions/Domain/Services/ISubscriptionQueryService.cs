using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Queries;

namespace RentalPeAPI.subscriptions.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<Subscription?> Handle(GetSubscriptionByIdQuery query);
    Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByCustomerIdQuery query);
    Task<IEnumerable<Subscription>> Handle(GetSubscriptionsByStatusQuery query);
}
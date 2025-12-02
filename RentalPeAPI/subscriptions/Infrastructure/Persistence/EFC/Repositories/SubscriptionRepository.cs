

using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.subscriptions.Domain.Model.Aggregates;
using RentalPeAPI.subscriptions.Domain.Model.Enums;
using RentalPeAPI.subscriptions.Domain.Repositories;

namespace RentalPeAPI.subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<IEnumerable<Subscription>> FindByCustomerIdAsync(int customerId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> FindByStatusAsync(SubscriptionStatus status)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> FindByStatusAndCustomerIdAsync(
        SubscriptionStatus status,
        int customerId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.Status == status && s.CustomerId == customerId)
            .ToListAsync();
    }
}
using RentalPeAPI.subscriptions.Domain.Model.Enums;
using RentalPeAPI.subscriptions.Domain.Model.ValueObjects;

namespace RentalPeAPI.subscriptions.Domain.Model.Aggregates;

public partial class Subscription
{
    public int Id { get; }

    public int CustomerId { get; private set; }

    public SubscriptionPlan Plan { get; private set; }

    public SubscriptionPrice Price { get; private set; } = null!;

    public SubscriptionStatus Status { get; private set; }

    public SubscriptionPeriod Period { get; private set; } = null!;

    // Para mapear fácil al JSON
    public DateTimeOffset StartDate => Period.StartDate;
    public DateTimeOffset EndDate   => Period.EndDate;

    protected Subscription()
    {
        Status = SubscriptionStatus.ACTIVE;
    }

    public Subscription(
        int customerId,
        SubscriptionPlan plan,
        SubscriptionPrice price,
        SubscriptionPeriod period)
    {
        if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
        Price  = price  ?? throw new ArgumentNullException(nameof(price));
        Period = period ?? throw new ArgumentNullException(nameof(period));

        CustomerId = customerId;
        Plan       = plan;
        Status     = SubscriptionStatus.ACTIVE;
    }

    public void ChangePlan(SubscriptionPlan newPlan, SubscriptionPrice newPrice)
    {
        if (Status != SubscriptionStatus.ACTIVE)
            throw new InvalidOperationException("Only ACTIVE subscriptions can change plan.");

        Plan  = newPlan;
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void Cancel()
    {
        if (Status == SubscriptionStatus.CANCELED) return;
        if (Status == SubscriptionStatus.EXPIRED)
            throw new InvalidOperationException("Cannot cancel an EXPIRED subscription.");

        Status = SubscriptionStatus.CANCELED;
    }

    public void Expire()
    {
        if (Status == SubscriptionStatus.EXPIRED) return;

        Status = SubscriptionStatus.EXPIRED;
    }

    public void Extend(DateTimeOffset newEndDate)
    {
        Period = Period.ExtendTo(newEndDate);
    }
}
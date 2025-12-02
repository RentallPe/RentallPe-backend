namespace RentalPeAPI.subscriptions.Domain.Model.ValueObjects;

public sealed record SubscriptionPeriod
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate   { get; }

    public SubscriptionPeriod(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException("EndDate must be greater than StartDate", nameof(endDate));

        StartDate = startDate;
        EndDate   = endDate;
    }

    public SubscriptionPeriod ExtendTo(DateTimeOffset newEndDate)
    {
        if (newEndDate <= StartDate)
            throw new ArgumentException("New end date must be greater than start date", nameof(newEndDate));

        return new SubscriptionPeriod(StartDate, newEndDate);
    }
}
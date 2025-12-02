namespace RentalPeAPI.subscriptions.Domain.Model.ValueObjects;

public sealed record SubscriptionPrice
{
    public decimal Amount { get; }

    public SubscriptionPrice(decimal amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be >= 0");
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
}
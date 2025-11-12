using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Domain.Model.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    private Money() { } 

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be >= 0");
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency;
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        var result = Amount - other.Amount;
        if (result < 0) throw new InvalidOperationException("Resulting amount cannot be negative");
        return new Money(result, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch");
    }
}
namespace RentalPeAPI.Combo.Domain.Aggregates.ValueObjects;

public record Price
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Price must be positive.", nameof(value));
        Value = value;
    }

    public override string ToString() => Value.ToString("F2");
}
namespace RentalPeAPI.Combo.Domain.Aggregates.ValueObjects;

public record InstallDays
{
    public int Value { get; }

    public InstallDays(int value)
    {
        if (value <= 0)
            throw new ArgumentException("InstallDays must be greater than zero.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value.ToString();
}
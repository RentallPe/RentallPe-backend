namespace RentalPeAPI.Payments.Domain.Model.ValueObjects;

public sealed class Monitoring
{
    public long Value { get; private set; }

    private Monitoring() { } // EF

    public Monitoring(long value)
    {
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }

    public Monitoring Increment(long delta = 1)
    {
        if (delta < 0) throw new ArgumentOutOfRangeException(nameof(delta));
        return new Monitoring(checked(Value + delta));
    }
}
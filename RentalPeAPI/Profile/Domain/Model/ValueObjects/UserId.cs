namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class UserId
{
    public long Value { get; private set; }

    private UserId() { } 

    public UserId(long value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
        Value = value;
    }
    
    public override bool Equals(object? obj) => obj is UserId other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
    public static bool operator ==(UserId a, UserId b) => a.Equals(b);
    public static bool operator !=(UserId a, UserId b) => !a.Equals(b);
}
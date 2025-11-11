namespace RentalPeAPI.Property.Domain.Aggregates.ValueObjects;

public record Location
{
    public string Address { get; }

    public Location(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Location cannot be empty.", nameof(address));
        Address = address;
    }

    public override string ToString() => Address;
}
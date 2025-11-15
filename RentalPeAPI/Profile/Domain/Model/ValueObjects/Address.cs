namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class Address
{
    public string Line1 { get; private set; }
    public string? Line2 { get; private set; }
    public string? District { get; private set; }
    public string City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }
    public string Country { get; private set; }

    private Address()
    {
        Line1 = string.Empty;
        City = string.Empty;
        Country = string.Empty;
    } 

    public Address(string line1, string city, string country,
        string? line2 = null, string? district = null,
        string? state = null, string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(line1)) throw new ArgumentException(nameof(line1));
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException(nameof(city));
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException(nameof(country));

        Line1 = line1.Trim();
        Line2 = string.IsNullOrWhiteSpace(line2) ? null : line2.Trim();
        District = string.IsNullOrWhiteSpace(district) ? null : district.Trim();
        City = city.Trim();
        State = string.IsNullOrWhiteSpace(state) ? null : state.Trim();
        PostalCode = string.IsNullOrWhiteSpace(postalCode) ? null : postalCode.Trim();
        Country = country.Trim();
    }
}
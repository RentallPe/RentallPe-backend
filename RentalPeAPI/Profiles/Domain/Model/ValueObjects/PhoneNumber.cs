namespace RentalPeAPI.Profiles.Domain.Model.ValueObjects;

public record PhoneNumber
{
    public PhoneNumber()
    {
        Number = string.Empty;
    }

    public PhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone cannot be empty.", nameof(number));

        Number = number.Trim();
    }

    public string Number { get; init; }
}
using System.Text.RegularExpressions;

namespace RentalPeAPI.Profiles.Domain.Model.ValueObjects;

public record EmailAddress
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public EmailAddress()
    {
        Address = string.Empty;
    }

    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email cannot be empty.", nameof(address));

        if (!EmailRegex.IsMatch(address))
            throw new ArgumentException("Invalid email format.", nameof(address));

        Address = address.Trim();
    }

    public string Address { get; init; }
}
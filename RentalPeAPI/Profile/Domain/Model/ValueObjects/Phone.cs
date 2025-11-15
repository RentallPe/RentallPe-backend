using System.Text.RegularExpressions;

namespace RentalPeAPI.Profile.Domain.Model.ValueObjects;

public sealed class Phone
{
    // E.164 (ej: +51987654321). Permite vacío si no se define.
    private static readonly Regex E164 = new(@"^\+?[1-9]\d{6,14}$", RegexOptions.Compiled);

    public string Number { get; private set; }

    private Phone() { Number = string.Empty; } // EF

    public Phone(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number required.", nameof(number));
        var n = number.Trim();
        if (!E164.IsMatch(n)) throw new ArgumentException("Invalid E.164 phone format.", nameof(number));
        Number = n;
    }
}
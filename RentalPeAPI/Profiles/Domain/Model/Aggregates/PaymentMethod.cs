namespace RentalPeAPI.Profiles.Domain.Model.Aggregates;

public class PaymentMethod
{
    protected PaymentMethod() { }

    public PaymentMethod(string type, string number, string expiry, string cvv)
    {
        Type = type;
        Number = number;
        Expiry = expiry;
        Cvv = cvv;
    }

    public long Id { get; private set; }

    public string Type { get; private set; } = string.Empty;

    public string Number { get; private set; } = string.Empty;

    public string Expiry { get; private set; } = string.Empty;

    public string Cvv { get; private set; } = string.Empty;
}
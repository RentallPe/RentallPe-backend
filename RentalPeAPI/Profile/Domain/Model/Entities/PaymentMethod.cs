namespace RentalPeAPI.Profile.Domain.Model.Entities;

/// <summary>
/// Método de pago asociado al perfil.
/// </summary>
public class PaymentMethod
{
    public int Id { get; private set; }

    public string Number { get; private set; } = string.Empty;
    public string ExpirationDate { get; private set; } = string.Empty;
    public string SecurityCode { get; private set; } = string.Empty;

    // ctor vacío para EF
    protected PaymentMethod() { }

    public PaymentMethod(string number, string expirationDate, string securityCode)
    {
        Number = number;
        ExpirationDate = expirationDate;
        SecurityCode = securityCode;
    }
}
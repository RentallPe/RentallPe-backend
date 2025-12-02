namespace RentalPeAPI.Payments.Interfaces.REST.Resources.payments;

public record PaymentResource(
    int Id,
    int ProjectId,
    int Installment,
    decimal Amount,
    DateTimeOffset Date,
    string Status,
    string CurrencySymbol);
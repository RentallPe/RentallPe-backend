namespace RentalPeAPI.Profiles.Domain.Model.Commands;

public record AddPaymentMethodCommand(
    int ProfileId,
    string Type,
    string Number,
    string Expiry,
    string Cvv);
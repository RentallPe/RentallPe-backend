namespace RentalPeAPI.Profiles.Interfaces.REST.Resources;

public record PaymentMethodResource(
    long Id,
    string Type,
    string Number,
    string Expiry,
    string Cvv);
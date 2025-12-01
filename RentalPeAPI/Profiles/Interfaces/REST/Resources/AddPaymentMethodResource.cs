namespace RentalPeAPI.Profiles.Interfaces.REST.Resources;

public record AddPaymentMethodResource(
    string Type,
    string Number,
    string Expiry,
    string Cvv);
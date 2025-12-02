namespace RentalPeAPI.Profiles.Interfaces.REST.Resources;

public record ProfileResource(
    int Id,
    string FullName,
    string Email,
    string Password,
    string Phone,
    DateTimeOffset? CreatedAt,
    string Photo,
    string Role,
    IEnumerable<PaymentMethodResource> PaymentMethods);
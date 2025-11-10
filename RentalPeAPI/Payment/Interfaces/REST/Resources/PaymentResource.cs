using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Interfaces.REST.Resources;


public record PaymentResource(
    int Id,
    int UserId,
    MoneyResource Money,
    PaymentMethodResource Method,
    PaymentStatus Status,
    string? Reference,
    DateTimeOffset? CreatedAt);
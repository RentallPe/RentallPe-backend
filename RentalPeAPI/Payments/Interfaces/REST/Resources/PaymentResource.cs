using RentalPeAPI.Payments.Domain.Model.Enums;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources;


public record PaymentResource(
    int Id,
    int UserId,
    MoneyResource Money,
    PaymentMethodResource Method,
    PaymentStatus Status,
    string? Reference,
    DateTimeOffset? CreatedAt);
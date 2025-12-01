using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources;

public record CreatePaymentResource(
    [Required] int UserId,
    [Required] MoneyResource Money,
    [Required] PaymentMethodResource Method,
    string? Reference);
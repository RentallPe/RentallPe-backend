using RentalPeAPI.Payment.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payment.Domain.Model.Commands.payments;


public sealed record CreatePaymentCommand(
    int UserId,
    Money Money,
    PaymentMethodSummary Method,
    string? Reference = null);
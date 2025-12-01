using RentalPeAPI.Payments.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payments.Domain.Model.Commands.payments;


public sealed record CreatePaymentCommand(
    int UserId,
    Money Money,
    PaymentMethodSummary Method,
    string? Reference = null);
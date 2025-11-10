using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Domain.Model.Queries.Payments;

public sealed record GetPaymentsByStatusQuery(PaymentStatus Status, int? UserId = null);
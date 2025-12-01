using RentalPeAPI.Payments.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payments.Domain.Model.Commands.Invoices;

public sealed record CreateInvoiceCommand(
    int PaymentId,
    int BookingId,
    int UserId,
    Money Total);
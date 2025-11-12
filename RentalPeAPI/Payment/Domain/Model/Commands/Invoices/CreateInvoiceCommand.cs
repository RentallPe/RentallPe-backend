using RentalPeAPI.Payment.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payment.Domain.Model.Commands.Invoices;

public sealed record CreateInvoiceCommand(
    int PaymentId,
    int BookingId,
    int UserId,
    Money Total);
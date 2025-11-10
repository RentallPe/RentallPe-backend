using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Interfaces.REST.Resources;

public record InvoiceResource(
    int Id,
    int PaymentId,
    int BookingId,
    int UserId,
    DateTimeOffset IssueDate,
    MoneyResource Total,
    InvoiceStatus Status,
    DateTimeOffset? CreatedAt);
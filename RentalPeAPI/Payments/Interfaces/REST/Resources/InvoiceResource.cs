using RentalPeAPI.Payments.Domain.Model.Enums;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources;

public record InvoiceResource(
    int Id,
    int PaymentId,
    int BookingId,
    int UserId,
    DateTimeOffset IssueDate,
    MoneyResource Total,
    InvoiceStatus Status,
    DateTimeOffset? CreatedAt);
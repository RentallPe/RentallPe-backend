using RentalPeAPI.Payment.Domain.Model.Enums;
using RentalPeAPI.Payment.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payment.Domain.Model.Aggregates;


public partial class Invoice
{
    public int Id { get; }
    public int PaymentId { get; private set; }
    public int BookingId { get; private set; }
    public int UserId { get; private set; }
    public DateTimeOffset IssueDate { get; private set; }
    public Money Total { get; private set; } = null!;
    public InvoiceStatus Status { get; private set; }

    protected Invoice()
    {
        Status = InvoiceStatus.DRAFT;
        IssueDate = DateTimeOffset.UtcNow;
    }

    public Invoice(int paymentId, int bookingId, int userId, Money total)
    {
        PaymentId = paymentId;
        BookingId = bookingId;
        UserId = userId;
        Total = total ?? throw new ArgumentNullException(nameof(total));
        Status = InvoiceStatus.DRAFT;
        IssueDate = DateTimeOffset.UtcNow;
    }

    public void GeneratePdf()
    {
        if (Status != InvoiceStatus.DRAFT)
            throw new InvalidOperationException("Only DRAFT invoices can be issued");
        Status = InvoiceStatus.ISSUED;
        IssueDate = DateTimeOffset.UtcNow;
    }

    public void SendEmail()
    {
        if (Status != InvoiceStatus.ISSUED)
            throw new InvalidOperationException("Only ISSUED invoices can be emailed");
    }

    public void Void()
    {
        if (Status == InvoiceStatus.VOID)
            return;
        if (Status != InvoiceStatus.DRAFT)
            throw new InvalidOperationException("Only DRAFT invoices can be voided");
        Status = InvoiceStatus.VOID;
    }
}
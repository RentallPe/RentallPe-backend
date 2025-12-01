using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payments.Domain.Model.Aggregates;

public partial class Payment
{
    public int Id { get; }
    public int UserId { get; private set; }
    public Money Money { get; private set; } = null!;
    public PaymentMethodSummary Method { get; private set; } = null!;
    public PaymentStatus Status { get; private set; }
    public string? Reference { get; private set; }

    protected Payment()
    {
        Status = PaymentStatus.PENDING;
    }

    public Payment(int userId, Money money, PaymentMethodSummary method, string? reference = null)
    {
        UserId = userId;
        Money = money ?? throw new ArgumentNullException(nameof(money));
        Method = method ?? throw new ArgumentNullException(nameof(method));
        Reference = string.IsNullOrWhiteSpace(reference) ? null : reference.Trim();
        Status = PaymentStatus.PENDING;
    }

    public void InitiatePayment()
    {
        if (Status != PaymentStatus.PENDING)
            throw new InvalidOperationException("Payment already initiated");
    }

    public void ConfirmPayment()
    {
        if (Status != PaymentStatus.PENDING)
            throw new InvalidOperationException("Only PENDING payments can be confirmed");
        Status = PaymentStatus.SETTLED; 
    }

    public void CancelPayment()
    {
        if (Status != PaymentStatus.PENDING)
            throw new InvalidOperationException("Only PENDING payments can be cancelled");
        Status = PaymentStatus.CANCELLED;
    }

    public void RefundPayment()
    {
        if (Status != PaymentStatus.SETTLED)
            throw new InvalidOperationException("Only SETTLED payments can be refunded");
        Status = PaymentStatus.REFUNDED;
    }
}
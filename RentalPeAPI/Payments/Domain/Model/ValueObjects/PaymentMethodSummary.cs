using RentalPeAPI.Payments.Domain.Model.Enums;

namespace RentalPeAPI.Payments.Domain.Model.ValueObjects;

public sealed class PaymentMethodSummary
{
    public PaymentMethodType Type { get; private set; }
    public string? Label { get; private set; }
    public string? Last4 { get; private set; }

    private PaymentMethodSummary() { } // EF

    public PaymentMethodSummary(PaymentMethodType type, string? label = null, string? last4 = null)
    {
        if (!string.IsNullOrEmpty(last4) && last4.Length != 4)
            throw new ArgumentException("last4 must have length 4", nameof(last4));

        Type = type;
        Label = string.IsNullOrWhiteSpace(label) ? null : label.Trim();
        Last4 = string.IsNullOrWhiteSpace(last4) ? null : last4;
    }
}
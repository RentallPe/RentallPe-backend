using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Model.ValueObjects;

namespace RentalPeAPI.Payments.Domain.Model.Aggregates;

public partial class Invoice
{
    public int Id { get; }

    /// <summary>
    /// Relación con el pago al que pertenece la factura.
    /// </summary>
    public int PaymentId { get; private set; }

    /// <summary>
    /// Número legible de la factura, por ejemplo "INV-001".
    /// </summary>
    public string Number { get; private set; } = string.Empty;

    /// <summary>
    /// Fecha de emisión lógica de la factura (issueDate en el JSON).
    /// </summary>
    public DateTimeOffset IssueDate { get; private set; }

    /// <summary>
    /// Estado interno de la factura, por si sigues usando los flujos de emisión/void.
    /// No se expone en el JSON actual, pero se mantiene a nivel de dominio.
    /// </summary>
    public InvoiceStatus Status { get; private set; }

    protected Invoice()
    {
        Status = InvoiceStatus.DRAFT;
        IssueDate = DateTimeOffset.UtcNow;
    }

    public Invoice(int paymentId, string number, DateTimeOffset? issueDate = null)
    {
        if (paymentId <= 0) throw new ArgumentOutOfRangeException(nameof(paymentId));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Invoice number is required", nameof(number));

        PaymentId = paymentId;
        Number = number.Trim();
        Status = InvoiceStatus.DRAFT;
        IssueDate = issueDate ?? DateTimeOffset.UtcNow;
    }

    public void ChangeNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Invoice number is required", nameof(number));

        if (Status != InvoiceStatus.DRAFT)
            throw new InvalidOperationException("Only DRAFT invoices can change number");

        Number = number.Trim();
    }

    public void SetIssueDate(DateTimeOffset issueDate)
    {
        if (Status != InvoiceStatus.DRAFT)
            throw new InvalidOperationException("Only DRAFT invoices can change issue date");

        IssueDate = issueDate;
    }

    public void GeneratePdf()
    {
        if (Status != InvoiceStatus.DRAFT)
            throw new InvalidOperationException("Only DRAFT invoices can be issued");
        if (string.IsNullOrWhiteSpace(Number))
            throw new InvalidOperationException("Invoice number must be assigned before issuing");

        Status = InvoiceStatus.ISSUED;
        // Si quieres que la emisión actualice la fecha a "hoy", la dejas así:
        IssueDate = DateTimeOffset.UtcNow;
        // Si quieres respetar la IssueDate previa, elimina la línea anterior.
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
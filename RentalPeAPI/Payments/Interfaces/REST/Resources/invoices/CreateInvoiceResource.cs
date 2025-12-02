using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources.invoices;

public record CreateInvoiceResource(
    [Required] int PaymentId,
    [Required] string Number,
    DateTimeOffset? IssueDate);
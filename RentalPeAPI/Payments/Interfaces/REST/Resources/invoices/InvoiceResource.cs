namespace RentalPeAPI.Payments.Interfaces.REST.Resources.invoices;

public record InvoiceResource(
    int Id,
    int PaymentId,
    string Number,
    DateTimeOffset IssueDate,
    DateTimeOffset? CreatedAt);
namespace RentalPeAPI.Payments.Domain.Model.Commands.Invoices;

public sealed record VoidInvoiceCommand(int InvoiceId);
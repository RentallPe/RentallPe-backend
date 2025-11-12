namespace RentalPeAPI.Payment.Domain.Model.Queries.Invoices;

public sealed record GetInvoicesByPaymentIdQuery(int PaymentId);
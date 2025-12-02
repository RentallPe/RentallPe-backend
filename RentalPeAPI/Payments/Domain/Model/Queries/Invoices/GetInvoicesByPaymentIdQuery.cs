namespace RentalPeAPI.Payments.Domain.Model.Queries.Invoices;

public sealed record GetInvoicesByPaymentIdQuery(int PaymentId);
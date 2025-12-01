using RentalPeAPI.Payments.Domain.Model.Enums;

namespace RentalPeAPI.Payments.Domain.Model.Queries.Invoices;

public sealed record GetInvoicesByStatusQuery(InvoiceStatus Status);
using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Domain.Model.Queries.Invoices;

public sealed record GetInvoicesByStatusQuery(InvoiceStatus Status, int? UserId = null);
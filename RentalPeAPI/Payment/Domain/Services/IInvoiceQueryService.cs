using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Domain.Model.Queries.Invoices;

namespace RentalPeAPI.Payment.Domain.Services;

public interface IInvoiceQueryService
{
    Task<Invoice?> Handle(GetInvoiceByIdQuery query);
    Task<IEnumerable<Invoice>> Handle(GetInvoicesByUserIdQuery query);
    Task<IEnumerable<Invoice>> Handle(GetInvoicesByPaymentIdQuery query);
    Task<IEnumerable<Invoice>> Handle(GetInvoicesByStatusQuery query);
}
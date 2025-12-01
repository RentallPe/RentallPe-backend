using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Queries.Invoices;

namespace RentalPeAPI.Payments.Domain.Services.invoice;

public interface IInvoiceQueryService
{
    Task<Invoice?> Handle(GetInvoiceByIdQuery query);

    Task<IEnumerable<Invoice>> Handle(GetInvoicesByUserIdQuery query);

    Task<IEnumerable<Invoice>> Handle(GetInvoicesByPaymentIdQuery query);

    Task<IEnumerable<Invoice>> Handle(GetInvoicesByStatusQuery query);
}
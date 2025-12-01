using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Queries.Invoices;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services.invoice;

namespace RentalPeAPI.Payments.Application.Internal.QueryServices;

public class InvoiceQueryService(IInvoiceRepository invoiceRepository) : IInvoiceQueryService
{
    public async Task<Invoice?> Handle(GetInvoiceByIdQuery query)
        => await invoiceRepository.FindByIdAsync(query.InvoiceId);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByUserIdQuery query)
        => await invoiceRepository.FindByUserIdAsync(query.UserId);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByPaymentIdQuery query)
        => await invoiceRepository.FindByPaymentIdAsync(query.PaymentId);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByStatusQuery query)
        => await invoiceRepository.FindByStatusAsync(query.Status);
}
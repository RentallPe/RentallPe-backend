using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Domain.Model.Queries.Invoices;
using RentalPeAPI.Payment.Domain.Repositories;
using RentalPeAPI.Payment.Domain.Services;

namespace RentalPeAPI.Payment.Application.Internal.QueryServices;

public class InvoiceQueryService(IInvoiceRepository invoiceRepository) : IInvoiceQueryService
{
    public async Task<Invoice?> Handle(GetInvoiceByIdQuery query)
        => await invoiceRepository.FindByIdAsync(query.Id);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByUserIdQuery query)
        => await invoiceRepository.FindByUserIdAsync(query.UserId);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByPaymentIdQuery query)
        => await invoiceRepository.FindByPaymentIdAsync(query.PaymentId);

    public async Task<IEnumerable<Invoice>> Handle(GetInvoicesByStatusQuery query)
        => await invoiceRepository.FindByStatusAsync(query.Status);
}
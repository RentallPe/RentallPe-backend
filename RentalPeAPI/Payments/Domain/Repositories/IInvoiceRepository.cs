using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payments.Domain.Repositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<IEnumerable<Invoice>> FindByUserIdAsync(int userId);
    Task<IEnumerable<Invoice>> FindByPaymentIdAsync(int paymentId);
    Task<IEnumerable<Invoice>> FindByStatusAsync(InvoiceStatus status);
}
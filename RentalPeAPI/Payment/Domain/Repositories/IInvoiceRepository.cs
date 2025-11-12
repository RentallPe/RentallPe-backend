using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payment.Domain.Repositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<IEnumerable<Invoice>> FindByUserIdAsync(int userId);
    Task<IEnumerable<Invoice>> FindByPaymentIdAsync(int paymentId);
    Task<IEnumerable<Invoice>> FindByStatusAsync(InvoiceStatus status);
}
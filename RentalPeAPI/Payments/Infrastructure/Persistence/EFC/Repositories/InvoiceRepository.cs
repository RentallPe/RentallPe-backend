using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;

public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Invoice>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Invoice>()
            .Join(
                Context.Set<Payment>(),
                invoice => invoice.PaymentId,
                payment => payment.Id,
                (invoice, payment) => new { invoice, payment })
            .Where(x => x.payment.UserId == userId)
            .Select(x => x.invoice)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> FindByPaymentIdAsync(int paymentId)
    {
        return await Context.Set<Invoice>()
            .Where(i => i.PaymentId == paymentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> FindByStatusAsync(InvoiceStatus status)
    {
        return await Context.Set<Invoice>()
            .Where(i => i.Status == status)
            .ToListAsync();
    }
}
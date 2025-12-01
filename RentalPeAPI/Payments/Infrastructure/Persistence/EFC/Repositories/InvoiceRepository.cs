using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;

public class InvoiceRepository(AppDbContext context)
    : BaseRepository<Invoice>(context), IInvoiceRepository
{
    private IInvoiceRepository _invoiceRepositoryImplementation;

    public async Task<IEnumerable<Invoice>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Invoice>()
            .Where(i => i.UserId == userId)
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
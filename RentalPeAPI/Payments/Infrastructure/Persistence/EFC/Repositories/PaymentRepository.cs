using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;

public class PaymentRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregates.Payment>(context), IPaymentRepository
{
    private IPaymentRepository _paymentRepositoryImplementation;

    public async Task<IEnumerable<Domain.Model.Aggregates.Payment>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Domain.Model.Aggregates.Payment>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<Domain.Model.Aggregates.Payment?> FindByReferenceAsync(string reference)
    {
        return await Context.Set<Domain.Model.Aggregates.Payment>()
            .FirstOrDefaultAsync(p => p.Reference == reference);
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Payment>> FindByStatusAsync(PaymentStatus status)
    {
        return await Context.Set<Domain.Model.Aggregates.Payment>()
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Model.Aggregates.Payment>> FindByStatusAndUserIdAsync(PaymentStatus status, int userId)
    {
        return await Context.Set<Domain.Model.Aggregates.Payment>()
            .Where(p => p.Status == status && p.UserId == userId)
            .ToListAsync();
    }
}
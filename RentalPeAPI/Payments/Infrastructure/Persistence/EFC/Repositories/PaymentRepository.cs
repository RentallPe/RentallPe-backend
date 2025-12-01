using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Payment>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Payment>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<Payment?> FindByReferenceAsync(string reference)
    {
        return await Context.Set<Payment>()
            .FirstOrDefaultAsync(p => p.Reference == reference);
    }

    public async Task<IEnumerable<Payment>> FindByStatusAsync(PaymentStatus status)
    {
        return await Context.Set<Payment>()
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> FindByStatusAndUserIdAsync(PaymentStatus status, int userId)
    {
        return await Context.Set<Payment>()
            .Where(p => p.Status == status && p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> FindByProjectIdAsync(int projectId)
    {
        return await Context.Set<Payment>()
            .Where(p => p.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<Payment?> FindByProjectAndInstallmentAsync(int projectId, int installment)
    {
        return await Context.Set<Payment>()
            .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.Installment == installment);
    }
}
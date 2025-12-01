using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Domain.Model.ValueObjects;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return await Context.Set<Profile>()
            .Include(p => p.PaymentMethods)
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    /// <summary>
    /// Optional: método helper para traer el perfil completo por Id con payment methods.
    /// </summary>
    public async Task<Profile?> FindByIdWithPaymentMethodsAsync(int id)
    {
        return await Context.Set<Profile>()
            .Include(p => p.PaymentMethods)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context)
    : BaseRepository<Domain.Model.Aggregates.Profile>(context), IProfileRepository
{
    public async Task<Domain.Model.Aggregates.Profile?> FindByUserIdAsync(long userId)
        => await Context.Set<Domain.Model.Aggregates.Profile>()
            .FirstOrDefaultAsync(p => p.UserId.Value == userId);

    public async Task<Domain.Model.Aggregates.Profile?> FindByEmailAsync(string email)
        => await Context.Set<Domain.Model.Aggregates.Profile>()
            .FirstOrDefaultAsync(p => p.PrimaryEmail == email);
}
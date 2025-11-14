using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Implementación con Entity Framework Core de <see cref="IProfileRepository"/>.
/// </summary>
public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Busca un perfil por el identificador del usuario (UserId de IAM).
    /// </summary>
    /// <param name="userId">Identificador de usuario (IAM).</param>
    public async Task<Profile?> FindByUserIdAsync(long userId)
    {
        return await Context.Set<Profile>()
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    /// <summary>
    /// Verifica si ya existe un perfil para el usuario dado.
    /// </summary>
    /// <param name="userId">Identificador de usuario (IAM).</param>
    public async Task<bool> ExistsByUserIdAsync(long userId)
    {
        return await Context.Set<Profile>()
            .AnyAsync(p => p.UserId == userId);
    }
}
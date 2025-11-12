using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;

using System.Threading;
using System.Threading.Tasks;




public sealed class UnitOfWorkAdapter : RentalPeAPI.User.Domain.Repositories.IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWorkAdapter(AppDbContext context) => _context = context;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Infrastructure.Persistence.EFC; 

namespace RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext _context;

    public UnitOfWork(UserDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
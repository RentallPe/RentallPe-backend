using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Infrastructure.Persistence.EFC; 
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 
namespace RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
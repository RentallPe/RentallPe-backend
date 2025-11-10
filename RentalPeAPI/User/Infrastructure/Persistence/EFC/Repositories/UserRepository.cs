using Microsoft.EntityFrameworkCore;
using RentalPeAPI.User.Domain;
using RentalPeAPI.User.Domain.Repositories;

namespace RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;


public class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(AppUser user)
    {
        await _context.Users.AddAsync(user);
    }
    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }
}
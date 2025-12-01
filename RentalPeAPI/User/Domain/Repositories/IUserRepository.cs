namespace RentalPeAPI.User.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> UserExistsByEmailAsync(string email);
    Task AddAsync(AppUser appUser);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByIdAsync(Guid id);
}
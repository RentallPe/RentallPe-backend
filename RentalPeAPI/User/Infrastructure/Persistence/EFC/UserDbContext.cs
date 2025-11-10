using Microsoft.EntityFrameworkCore;
using System.Reflection; 
using RentalPeAPI.User.Domain; 
namespace RentalPeAPI.User.Infrastructure.Persistence.EFC;

public class UserDbContext : DbContext
{
   
    public DbSet<AppUser> Users { get; set; }
    
  
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
       
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
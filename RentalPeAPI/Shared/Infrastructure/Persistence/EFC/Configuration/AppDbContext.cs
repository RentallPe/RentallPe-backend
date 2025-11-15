using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions; 
using RentalPeAPI.User.Domain; 
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Configuration; 
using RentalPeAPI.Property.Domain.Aggregates; 
using RentalPeAPI.Property.Domain.Aggregates.Entities; 
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Configuration; 
using RentalPeAPI.Payment.Infrastructure.Persistence.EFC.configuration.extensions;
using RentalPeAPI.Profile.Infrastructure.Persistence.EFC.configuration.extensions;

namespace RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 

/// <summary>
/// Represents the application's database context using Entity Framework Core.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   
    public DbSet<AppUser> Users { get; set; } 
    public DbSet<Space> Spaces { get; set; } 
    public DbSet<Service> Services { get; set; } 


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // --- ESTRUCTURA BASE DE FRAMEWORK (Remote) ---
        // Publishing Context
      //builder.ApplyPublishingConfiguration();
      // Profiles Context
      //builder.ApplyProfilesConfiguration();
        
        // --- APLICACIÓN DE CONFIGURACIONES (Combinación de todos los BCs) ---
        
        // 1. User BC (Reglas de User)
        builder.ApplyConfiguration(new UserConfiguration()); 
        
        
        builder.ApplyPaymentsConfiguration();
        builder.ApplyProfilesConfiguration();
        
        
        // 3. Space BC (De derecha)
        builder.ApplyConfiguration(new SpaceConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
        
        // Configuración compartida
        builder.UseSnakeCaseNamingConvention(); 

       

       
    }
}
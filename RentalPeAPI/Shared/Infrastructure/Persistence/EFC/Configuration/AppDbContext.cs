// --- USINGS NECESARIOS ---
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions; // <-- 1. ARREGLO: Faltaba este 'using' para SnakeCase
// Usings para tu BC de Property (Space)
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Aggregates.Entities;
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Configuration; 

// --- 2. ARREGLO: El namespace debe coincidir con la carpeta (EFC) ---
namespace RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // --- 3. ARREGLO: Añade el DbSet para tu BC ---
    public DbSet<Space> Spaces { get; set; } 
    public DbSet<Service> Services { get; set; } 
    // (Aquí se añadirán AppUser, Payment, etc.)

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // --- 4. ARREGLO: Aplica la configuración de tu BC ---
        builder.ApplyConfiguration(new SpaceConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());


        // (Aquí se añadirán UserConfiguration, etc.)
        
        // Configuración compartida
        builder.UseSnakeCaseNamingConvention(); // <-- Esta línea ahora funcionará
    }
}
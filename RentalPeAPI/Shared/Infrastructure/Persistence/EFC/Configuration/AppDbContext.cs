using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions; // NECESARIO para UseSnakeCaseNamingConvention

using RentalPeAPI.Combo.Domain.Aggregates.Entities;
using RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Configuration;

// --- 2. ARREGLO: El namespace debe coincidir con la carpeta (EFC) ---
// --- USINGS COMBINADOS (De ambos BCs) ---
using RentalPeAPI.User.Domain; // Para AppUser (De izquierda)
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Configuration; // Para UserConfiguration (De izquierda)
using RentalPeAPI.Property.Domain.Aggregates; // Para Space (De derecha)
using RentalPeAPI.Property.Domain.Aggregates.Entities; // Para Service (De derecha)
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Configuration; // Para SpaceConfiguration (De derecha)
using RentalPeAPI.Payment.Infrastructure.Persistence.EFC.configuration.extensions; // Para ApplyPaymentsConfiguration (De izquierda)
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; 

/// <summary>
/// Represents the application's database context using Entity Framework Core.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // --- 3. ARREGLO: Añade el DbSet para tu BC ---

    public DbSet<Combo.Domain.Aggregates.Entities.Combo> Combos { get; set; } = default!;
    
    // (Aquí se añadirán AppUser, Payment, etc.)

    // --- DBSETS COMBINADOS (USER, SPACE, SERVICE) ---
    public DbSet<AppUser> Users { get; set; } // De izquierda
    public DbSet<Space> Spaces { get; set; } // De derecha
    public DbSet<Service> Services { get; set; } // De derecha (Asumo que Service es un DbSet)
    public DbSet<IoTDevice> IoTDevices { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Incident> Incidents { get; set; } // <--- ¡Añade esto!
    public DbSet<Reading> Readings { get; set; } // <--- ¡Añade esto!
    public DbSet<WorkItem> Tasks { get; set; } // <--- ¡Añade esto!
    public DbSet<Notification> Notifications { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuraciones de Property
        // --- ESTRUCTURA BASE DE FRAMEWORK (Remote) ---
        // Publishing Context
      //builder.ApplyPublishingConfiguration();
      // Profiles Context
      //builder.ApplyProfilesConfiguration();
        
        // --- APLICACIÓN DE CONFIGURACIONES (Combinación de todos los BCs) ---
        
        // 1. User BC (Reglas de User)
        builder.ApplyConfiguration(new UserConfiguration()); 
        
        // 2. Payment BC (De izquierda)
        builder.ApplyPaymentsConfiguration();
        
        // 3. Space BC (De derecha)
        builder.ApplyConfiguration(new SpaceConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
       
        builder.ApplyConfiguration(new ProjectConfiguration()); 
        builder.ApplyConfiguration(new IoTDeviceConfiguration());
        
        builder.ApplyConfiguration(new ReadingConfiguration());
        builder.ApplyConfiguration(new WorkItemConfiguration());
        builder.ApplyConfiguration(new IncidentConfiguration());
        builder.ApplyConfiguration(new NotificationConfiguration());

        // Configuración de Combo
        builder.ApplyConfiguration(new ComboConfiguration());

        // (Aquí se añadirán UserConfiguration, etc.)

        // Configuración compartida
        builder.UseSnakeCaseNamingConvention();

       

       
    }

}
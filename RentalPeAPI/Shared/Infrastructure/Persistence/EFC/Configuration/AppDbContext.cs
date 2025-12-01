using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.User.Domain; 
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Configuration; 
using RentalPeAPI.Property.Domain.Aggregates; 
using RentalPeAPI.Property.Domain.Aggregates.Entities; 
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Configuration;
using RentalPeAPI.Profile.Infrastructure.Persistence.EFC.configuration.extensions;
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
// Para ApplyPaymentsConfiguration (De izquierda)
using RentalPeAPI.Monitoring.Domain.Entities;
using RentalPeAPI.Monitoring.Domain.Model.Aggregates;
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Configuration;
using RentalPeAPI.Payments.Infrastructure.Persistence.EFC.configuration.extensions;

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
    public DbSet<User.Domain.User> Users { get; set; }              // De izquierda
   

    public DbSet<Space> Spaces { get; set; }               // De derecha
    public DbSet<Service> Services { get; set; }           // De derecha
    public DbSet<IoTDevice> IoTDevices { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Incident> Incidents { get; set; } // <--- ¡Añade esto!
    public DbSet<Reading> Readings { get; set; } // <--- ¡Añade esto!
    public DbSet<WorkItem> Tasks { get; set; } // <--- ¡Añade esto!
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="builder">The model builder.</param>
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
        builder.ApplyConfiguration(new PaymentMethodConfiguration());
        // 🔗 Relación User (1) ─── (*) PaymentMethods
        
        
        // 2. Payment BC (De izquierda)
        builder.ApplyPaymentsConfiguration();
        builder.ApplyProfilesConfiguration();
        
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
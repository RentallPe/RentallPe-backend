using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MediatR;

// Shared
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           // AppDbContext
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;           // UnitOfWork impl
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           // KebabCase routes
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

// Usings del BC de Combo
using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Repositories;


// Usings del BC de Property (que ya tenías)
// Payment BC
using RentalPeAPI.Payment.Domain.Repositories;
using RentalPeAPI.Payment.Domain.Services;
using RentalPeAPI.Payment.Application.Internal.CommandServices;
using RentalPeAPI.Payment.Application.Internal.QueryServices;
using RentalPeAPI.Payment.Infrastructure.Persistence.EFC.Repositories;

// User BC
using RentalPeAPI.User.Application.Internal.CommandServices;
using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Domain.Services;
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.User.Infrastructure.Security;

// Property/Space BC (Tu rama)
using RentalPeAPI.Property.Application.Services;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Repositories;

// Profile BC
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Application.Internal.CommandServices;
using RentalPeAPI.Profile.Application.Internal.QueryServices;
using RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Property.Infrastructure.Persistence; // Asumo que es necesaria para EFCore.Repositories
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Repositories; // Para IWorkItemRepository
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories; 
using RentalPeAPI.Monitoring.Infrastructure.Services;
using RentalPeAPI.Monitoring.Domain.Services;
// Mapeo de UnitOfWork (Para evitar conflictos de ACME)
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);

// --- Servicios Compartidos y UI ---
builder.Services.AddLocalization();
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());

// --- CONFIGURACIÓN DE BASE DE DATOS (MANTENIENDO EL ESTÁNDAR) ---
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new Exception("Database connection string not found.");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySQL(cs)
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging()
       .EnableDetailedErrors());

// --- INYECCIÓN DE DEPENDENCIAS (COMBINADA) ---

// 1. Shared (IUnitOfWork)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();                          

// 2. User BC
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly); // User
    cfg.RegisterServicesFromAssembly(typeof(ComboCommandService).Assembly); // Combo
    cfg.RegisterServicesFromAssembly(typeof(PaymentCommandService).Assembly); // Payment
    // etc.
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService, TokenGenerationService>();
// builder.Services.AddScoped<RentalPeAPI.User.Domain.Repositories.IUnitOfWork, UnitOfWorkAdapter>(); // Mantengo el que ya estaba en la izquierda si es necesario

// 3. Payment BC
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService,   PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService,   InvoiceQueryService>();

// 4. Property/Space BC
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

// Profile BC
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IPreferenceSetRepository, PreferenceSetRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService,   ProfileQueryService>();
builder.Services.AddScoped<IPreferenceSetCommandService, PreferenceSetCommandService>();
builder.Services.AddScoped<IPreferenceSetQueryService,   PreferenceSetQueryService>();
// --- Servicios de Monitoring BC ---
builder.Services.AddScoped<IReadingRepository, ReadingRepository>();
builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IAnomalyDetectorService, AnomalyDetectorService>();
builder.Services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// 🔹 Servicios de aplicación y repositorios de Combo
builder.Services.AddScoped<ComboCommandService>();
builder.Services.AddScoped<ComboQueryService>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();


// 🔹 Agregamos el DbContext (Tu código ya estaba bien)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new Exception("Database connection string not found.");



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    if (builder.Environment.IsDevelopment())
    {
        options.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
});


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// --- EJECUCIÓN DE BASE DE DATOS (MANTIENE LA REGLA DEL EQUIPO) ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Mantenemos EnsureCreated() según la regla de la rama principal
    db.Database.EnsureCreated();        
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
// ... (El resto del Middleware de Localization, HttpsRedirection, y MapControllers)

// Localization
var cultures = new[] { "en", "en-US", "es", "es-PE" };
var loc = new RequestLocalizationOptions()
    .SetDefaultCulture(cultures[0])
    .AddSupportedCultures(cultures)
    .AddSupportedUICultures(cultures);
loc.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(loc);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
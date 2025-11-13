using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MediatR;

// --- USINGS COMBINADOS Y CORRECTOS ---
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC; // Para AppDbContext

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
using RentalPeAPI.Property.Infrastructure.Persistence; // Asumo que es necesaria para EFCore.Repositories

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
    // Usamos MySQL con el paquete de Pomelo o el compatible (que resuelve conflictos)
    opt.UseMySQL(cs) 
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging()
       .EnableDetailedErrors());

// --- INYECCIÓN DE DEPENDENCIAS (COMBINADA) ---

// 1. Shared (IUnitOfWork)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();                          

// 2. User BC
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
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
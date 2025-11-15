using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MediatR;

// Shared
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           // AppDbContext
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;           // UnitOfWork
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           // KebabCase routes
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

// Combo BC
using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Repositories;

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

// Property/Space BC
using RentalPeAPI.Property.Application.Services;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Repositories;

// Profile BC
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Application.Internal.CommandServices;
using RentalPeAPI.Profile.Application.Internal.QueryServices;
using RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;

// Monitoring BC
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Monitoring.Infrastructure.Services;
using RentalPeAPI.Monitoring.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + localization
builder.Services.AddLocalization();
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Swagger (solo Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());

// DbContext (Pomelo unificado)
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new Exception("Database connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(cs, ServerVersion.AutoDetect(cs));
    if (builder.Environment.IsDevelopment())
    {
        options.LogTo(Console.WriteLine, LogLevel.Information)
               .EnableSensitiveDataLogging()
               .EnableDetailedErrors();
    }
});

// DI compartido
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// MediatR (ensambla handlers de varios BC)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);     // User
    cfg.RegisterServicesFromAssembly(typeof(ComboCommandService).Assembly);     // Combo
    cfg.RegisterServicesFromAssembly(typeof(PaymentCommandService).Assembly);   // Payment
    cfg.RegisterServicesFromAssembly(typeof(ProfileCommandService).Assembly);   // Profile
});

// User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService,   TokenGenerationService>();

// Payment
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository,  InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService,  PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService,    PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService,  InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService,    InvoiceQueryService>();

// Property/Space
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

// Profile
builder.Services.AddScoped<IProfileRepository,       ProfileRepository>();
builder.Services.AddScoped<IPreferenceSetRepository, PreferenceSetRepository>();
builder.Services.AddScoped<IProfileCommandService,       ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService,         ProfileQueryService>();
builder.Services.AddScoped<IPreferenceSetCommandService, PreferenceSetCommandService>();
builder.Services.AddScoped<IPreferenceSetQueryService,   PreferenceSetQueryService>();

// Monitoring
builder.Services.AddScoped<IReadingRepository,      ReadingRepository>();
builder.Services.AddScoped<IWorkItemRepository,     WorkItemRepository>();
builder.Services.AddScoped<IIncidentRepository,     IncidentRepository>();
builder.Services.AddScoped<IProjectRepository,      ProjectRepository>();
builder.Services.AddScoped<IIoTDeviceRepository,    IoTDeviceRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAnomalyDetectorService, AnomalyDetectorService>();
builder.Services.AddScoped<INotificationService,    NotificationService>();

// Kestrel: solo HTTP para evitar warning de certificado y mixed content
builder.WebHost.ConfigureKestrel(o => o.ListenLocalhost(52888));

var app = builder.Build();

// EnsureCreated
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalPe API v1");
});

// Localization
var cultures = new[] { "en", "en-US", "es", "es-PE" };
var loc = new RequestLocalizationOptions()
    .SetDefaultCulture(cultures[0])
    .AddSupportedCultures(cultures)
    .AddSupportedUICultures(cultures);
loc.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(loc);

// Pipeline
// app.UseHttpsRedirection(); // deshabilitado: solo HTTP
app.UseAuthorization();
app.MapControllers();

app.Run();

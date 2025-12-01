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
using RentalPeAPI.Property.Infrastructure.Persistence; // Asumo que es necesaria para EFCore.Repositories
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Domain.Repositories; // Para IWorkItemRepository
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories; 
using RentalPeAPI.Monitoring.Infrastructure.Services;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Profiles.Application.Internal.CommandServices;
using RentalPeAPI.Profiles.Application.Internal.QueryServices;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Profiles.Infrastructure.Persistence.EFC.Repositories;

// Mapeo de UnitOfWork (Para evitar conflictos de ACME)

var builder = WebApplication.CreateBuilder(args);

#region MVC + Localization + Swagger

builder.Services
    .AddLocalization()
    .AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    
    c.CustomSchemaIds(type =>
        type.FullName!
            .Replace("RentalPeAPI.", string.Empty) 
            .Replace("+", ".")                      
            .Replace(".", "_"));                    
});

#endregion

#region Database

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new Exception("Database connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

    if (builder.Environment.IsDevelopment())
    {
        options
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
});

#endregion

#region Shared

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region MediatR (User, Combo, Payment, etc.)

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);      // User
    cfg.RegisterServicesFromAssembly(typeof(ComboCommandService).Assembly);      // Combo
    cfg.RegisterServicesFromAssembly(typeof(PaymentCommandService).Assembly);    // Payment
    // Agrega aquí más assemblies si lo necesitas
});

#endregion

#region User BC

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService, TokenGenerationService>();

#endregion

#region Payment BC

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();

#endregion

#region Property/Space BC

builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

#endregion

#region Profiles BC

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

#endregion

#region Monitoring BC

builder.Services.AddScoped<IReadingRepository, ReadingRepository>();
builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAnomalyDetectorService, AnomalyDetectorService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

#endregion

#region Combo BC

builder.Services.AddScoped<IComboRepository, ComboRepository>();
builder.Services.AddScoped<ComboCommandService>();
builder.Services.AddScoped<ComboQueryService>();

#endregion

var app = builder.Build();

#region Database assurance

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

#endregion

#region Swagger + OpenAPI

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

#region Localization

var cultures = new[] { "en", "en-US", "es", "es-PE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(cultures[0])
    .AddSupportedCultures(cultures)
    .AddSupportedUICultures(cultures);

localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);

#endregion

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
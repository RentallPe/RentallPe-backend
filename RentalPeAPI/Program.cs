using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MediatR;

// Shared
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           // AppDbContext
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;           // UnitOfWork
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           // KebabCase routes
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

// Monitoring ACL
using RentalPeAPI.Monitoring.Application.ACL;

// Combo BC
using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Repositories;

// Payment BC
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services;
using RentalPeAPI.Payments.Application.Internal.CommandServices;
using RentalPeAPI.Payments.Application.Internal.QueryServices;
using RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;

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

// Profiles BC
using RentalPeAPI.Profiles.Application.Internal.CommandServices;
using RentalPeAPI.Profiles.Application.Internal.QueryServices;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Profiles.Infrastructure.Persistence.EFC.Repositories;

// Monitoring BC
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Monitoring.Infrastructure.Services;
using RentalPeAPI.Monitoring.Domain.Services;
using RentalPeAPI.Payments.Domain.Services.invoice;
using RentalPeAPI.Payments.Domain.Services.payment;
using RentalPeAPI.subscriptions.Application.ACL;
using RentalPeAPI.subscriptions.Application.Internal.CommandServices;
using RentalPeAPI.subscriptions.Application.Internal.QueryServices;
using RentalPeAPI.subscriptions.Domain.Repositories;
using RentalPeAPI.subscriptions.Domain.Services;
using RentalPeAPI.subscriptions.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.subscriptions.Interfaces.ACL;

// Providers BC
using RentalPeAPI.Providers.Application.ACL;
using RentalPeAPI.Providers.Application.Internal.CommandServices;
using RentalPeAPI.providers.Domain.Repositories;
using RentalPeAPI.providers.Domain.Services;
using RentalPeAPI.providers.Infrastructure.Persistence.EFC.Repositories;


var builder = WebApplication.CreateBuilder(args);

// MVC + localization
builder.Services.AddLocalization();
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Swagger (solo Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.CustomSchemaIds(type =>
        type.FullName!
            .Replace("RentalPeAPI.", string.Empty)
            .Replace("+", ".")
            .Replace(".", "_"));
});

// Monitoring ACL
builder.Services.AddScoped<MonitoringContextFacade>();

// Función para añadir DbContext, simplificando la lógica
void AddMySqlDbContext(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
{
    var cs = configuration.GetConnectionString("DefaultConnection")
             ?? throw new Exception(
                 $"Database connection string 'DefaultConnection' not found in {environment.EnvironmentName}.");

    services.AddDbContext<AppDbContext>(options =>
    {
        // Usamos la sobrecarga que permite pasar opciones de MySQL
        options.UseMySql(cs, ServerVersion.AutoDetect(cs), mySqlOptions =>
        {
            // Opcional: Esto ayuda a que la aplicación no se caiga por fallas temporales
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        });

        if (environment.IsDevelopment())
        {
            options.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else // Producción
        {
            options.LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
    });
}

// Llama a la función de configuración
AddMySqlDbContext(builder.Services, builder.Configuration, builder.Environment);

// DI compartido
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// MediatR (ensambla handlers de varios BC)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);   // User
    cfg.RegisterServicesFromAssembly(typeof(ComboCommandService).Assembly);   // Combo
    cfg.RegisterServicesFromAssembly(typeof(PaymentCommandService).Assembly); // Payment
    cfg.RegisterServicesFromAssembly(typeof(ProfileCommandService).Assembly); // Profiles
});

// User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService,   TokenGenerationService>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

// Combo
builder.Services.AddScoped<IComboRepository, ComboRepository>();
builder.Services.AddScoped<ComboCommandService>();
builder.Services.AddScoped<ComboQueryService>();

// Payment
builder.Services.AddScoped<IPaymentRepository,      PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository,      InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService,  PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService,    PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService,  InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService,    InvoiceQueryService>();

// Property/Space
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

// Profile
builder.Services.AddScoped<IProfileRepository,       ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService,       ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService,         ProfileQueryService>();

// --- Subscriptions BC ---
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService,   SubscriptionQueryService>();
builder.Services.AddScoped<ISubscriptionsContextFacade, SubscriptionsContextFacade>();

// Monitoring
builder.Services.AddScoped<IReadingRepository,      ReadingRepository>();
builder.Services.AddScoped<IWorkItemRepository,     WorkItemRepository>();
builder.Services.AddScoped<IIncidentRepository,     IncidentRepository>();
builder.Services.AddScoped<IProjectRepository,      ProjectRepository>();
builder.Services.AddScoped<IIoTDeviceRepository,    IoTDeviceRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAnomalyDetectorService, AnomalyDetectorService>();
builder.Services.AddScoped<INotificationService,    NotificationService>();

// Providers
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IProviderCommandService, ProviderCommandService>();
builder.Services.AddScoped<IProviderQueryService, ProviderQueryService>();
builder.Services.AddScoped<ProvidersContextFacade>();

// Kestrel: solo HTTP para evitar warning de certificado y mixed content
//builder.WebHost.ConfigureKestrel(o => o.ListenLocalhost(52888));

var app = builder.Build();

// EnsureCreated
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger solo en Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Localization
var cultures = new[] { "en", "en-US", "es", "es-PE" };
var loc = new RequestLocalizationOptions()
    .SetDefaultCulture(cultures[0])
    .AddSupportedCultures(cultures)
    .AddSupportedUICultures(cultures);
loc.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(loc);

// Redirección raíz a Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger", permanent: true);
    return Task.CompletedTask;
});
// Pipeline
// app.UseHttpsRedirection(); // deshabilitado: solo HTTP
app.UseAuthorization();
app.MapControllers();

app.Run();

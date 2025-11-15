using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MediatR;

// Shared
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           // AppDbContext
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories;           // UnitOfWork impl
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           // KebabCase routes
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

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

var builder = WebApplication.CreateBuilder(args);

// Localization + MVC
builder.Services.AddLocalization();
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());

// DbContext
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new Exception("Database connection string not found.");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySQL(cs)
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging()
       .EnableDetailedErrors());

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// User BC
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService, TokenGenerationService>();

// Payment BC
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService,   PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService,   InvoiceQueryService>();

// Property/Space BC
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

// Profile BC
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IPreferenceSetRepository, PreferenceSetRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService,   ProfileQueryService>();
builder.Services.AddScoped<IPreferenceSetCommandService, PreferenceSetCommandService>();
builder.Services.AddScoped<IPreferenceSetQueryService,   PreferenceSetQueryService>();

var app = builder.Build();

// Ensure DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

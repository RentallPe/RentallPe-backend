using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;           
using RentalPeAPI.Shared.Infrastructure.Interfaces.ASP.Configuration;           

using RentalPeAPI.Payment.Domain.Repositories;
using RentalPeAPI.Payment.Domain.Services;
using RentalPeAPI.Payment.Application.Internal.CommandServices;
using RentalPeAPI.Payment.Application.Internal.QueryServices;
using RentalPeAPI.Payment.Infrastructure.Persistence.EFC.Repositories;

using RentalPeAPI.User.Application.Internal.CommandServices;
using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Domain.Services;
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.User.Infrastructure.Persistence.EFC;                          
using RentalPeAPI.User.Infrastructure.Security;
using IUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = ACME.LearningCenterPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization();
builder.Services.AddControllers(o => o.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(o => o.EnableAnnotations());


var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new Exception("Database connection string not found.");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySQL(cs) // Oracle provider
       .LogTo(Console.WriteLine, LogLevel.Information)
       .EnableSensitiveDataLogging()
       .EnableDetailedErrors());

// ---- MediatR (User BC) ----
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

// ---- DI: Shared ----
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();                          

// ---- DI: Payments ----
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService,   PaymentQueryService>();
builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
builder.Services.AddScoped<IInvoiceQueryService,   InvoiceQueryService>();

// ---- DI: User ----
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService,    TokenGenerationService>();
builder.Services.AddScoped<RentalPeAPI.User.Domain.Repositories.IUnitOfWork, UnitOfWorkAdapter>(); // SaveChangesAsync -> DbContext

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();        
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

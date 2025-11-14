// --- USINGS NECESARIOS ---
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration; // <-- Para AppDbContext
using RentalPeAPI.Shared.Domain.Repositories; // <-- Para IUnitOfWork
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Repositories; // <-- Para UnitOfWork

// Usings del BC de Combo
using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Combo.Infrastructure.Persistence.EFC.Repositories;


// Usings del BC de Property (que ya tenías)
using RentalPeAPI.Property.Application.Services;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Property.Infrastructure.Persistence;
using RentalPeAPI.Property.Infrastructure.Persistence.EFC.Repositories;
// --- FIN DE USINGS ---

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔹 Aquí registras tus servicios de aplicación y repositorios
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();


// 🔹 Servicios de aplicación y repositorios de Combo
builder.Services.AddScoped<ComboCommandService>();
builder.Services.AddScoped<ComboQueryService>();
builder.Services.AddScoped<IComboRepository, ComboRepository>();


// 🔹 Registra el UnitOfWork compartido (¡necesario para tu SpaceAppService!)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al migrar la base de datos.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
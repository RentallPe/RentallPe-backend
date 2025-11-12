
using MediatR;
using RentalPeAPI.User.Application.Internal.CommandServices;
using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Domain.Services;
using RentalPeAPI.User.Infrastructure.Persistence.EFC;
using RentalPeAPI.User.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.User.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)) 
);

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));




















builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddSingleton<ITokenGenerationService, TokenGenerationService>();




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
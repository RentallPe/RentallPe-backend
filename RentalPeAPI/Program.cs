using Microsoft.EntityFrameworkCore;
using RentalPeAPI.Property.Application.Services;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Property.Infrastructure.Persistence;
using RentalPeAPI.Property.Infrastructure.Persistence.EFCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔹 Aquí registras tus servicios de aplicación y repositorios
builder.Services.AddScoped<SpaceAppService>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();

// 🔹 Agregamos el DbContext
builder.Services.AddDbContext<PropertyDbContext>(options =>
        options.UseInMemoryDatabase("RentalPeDB") // o UseSqlServer, UseNpgsql, etc.
);

// OpenAPI (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
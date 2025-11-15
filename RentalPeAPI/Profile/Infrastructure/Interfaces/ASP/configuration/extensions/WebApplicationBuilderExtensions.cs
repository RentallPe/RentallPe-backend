using RentalPeAPI.Profile.Application.Internal.CommandServices;
using RentalPeAPI.Profile.Application.Internal.QueryServices;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Infrastructure.Persistence.EFC.Repositories;

namespace RentalPeAPI.Profile.Infrastructure.Interfaces.ASP.Configuration.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// Registra los servicios del bounded context Profile en el contenedor DI.
        /// </summary>
        /// <param name="builder">Instancia de WebApplicationBuilder.</param>
        public static void AddProfilesContextServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            
            builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
            builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
        }
    }
}
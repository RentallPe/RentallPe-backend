using RentalPeAPI.Profiles.Application.ACL;
using RentalPeAPI.Profiles.Application.Internal.CommandServices;
using RentalPeAPI.Profiles.Application.Internal.QueryServices;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Profiles.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Profiles.Interfaces.ACL;

namespace RentalPeAPI.Profiles.Infrastructure.Interfaces.ASP.configuration.extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddProfilesContextServices(this WebApplicationBuilder builder)
    {
        // Profiles Bounded Context Dependency Injection Configuration
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

        builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
        builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

        builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
    }
}
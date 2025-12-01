using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Domain.Model.Queries;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Profiles.Domain.Services;

namespace RentalPeAPI.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindProfileByEmailAsync(query.Email);
    }
}
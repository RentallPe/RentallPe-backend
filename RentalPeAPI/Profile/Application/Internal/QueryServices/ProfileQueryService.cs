using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;

namespace RentalPeAPI.Profile.Application.Internal.QueryServices;

/// <summary>
/// Implements the profile query service for the RentalPe platform.
/// </summary>
public class ProfileQueryService : IProfileQueryService
{
    private readonly IProfileRepository _profileRepository;

    public ProfileQueryService(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    /// <summary>
    /// Handles the get profile by id query.
    /// </summary>
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await _profileRepository.FindByIdAsync(query.ProfileId);
    }

    /// <summary>
    /// Handles the get profile by user id query.
    /// </summary>
    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await _profileRepository.FindByUserIdAsync(query.UserId);
    }
}
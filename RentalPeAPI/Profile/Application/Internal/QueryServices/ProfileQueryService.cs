using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;

namespace RentalPeAPI.Profile.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    public Task<Domain.Model.Aggregates.Profile?> Handle(GetProfileByIdQuery query)
        => profileRepository.FindByIdAsync(query.Id);

    public Task<Domain.Model.Aggregates.Profile?> Handle(GetProfileByUserIdQuery query)
        => profileRepository.FindByUserIdAsync(query.UserId);

    public Task<Domain.Model.Aggregates.Profile?> Handle(GetProfileByEmailQuery query)
        => profileRepository.FindByEmailAsync(query.Email);
}
using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Domain.Model.Queries;

namespace RentalPeAPI.Profiles.Domain.Services;

public interface IProfileQueryService
{
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);

    Task<Profile?> Handle(GetProfileByIdQuery query);

    Task<Profile?> Handle(GetProfileByEmailQuery query);
}
using RentalPeAPI.Profile.Domain.Model.Queries;

namespace RentalPeAPI.Profile.Domain.Services;

public interface IProfileQueryService
{
    Task<Model.Aggregates.Profile?> Handle(GetProfileByIdQuery query);
    Task<Model.Aggregates.Profile?> Handle(GetProfileByUserIdQuery query);
    Task<Model.Aggregates.Profile?> Handle(GetProfileByEmailQuery query);
}
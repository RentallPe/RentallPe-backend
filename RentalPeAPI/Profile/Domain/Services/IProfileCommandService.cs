using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Queries;

namespace RentalPeAPI.Profile.Domain.Services
{
    public interface IProfileQueryService
    {
        Task<Model.Aggregates.Profile?> Handle(GetProfileByIdQuery query);
        Task<Model.Aggregates.Profile?> Handle(GetProfileByUserIdQuery query);
    }
}
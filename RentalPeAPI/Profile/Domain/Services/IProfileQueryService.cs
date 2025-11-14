using System.Threading.Tasks;
using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Queries;

namespace RentalPeAPI.Profile.Domain.Services
{
    /// <summary>Servicio de consultas para Profile.</summary>
    public interface IProfileQueryService
    {
        Task<UserProfile?> Handle(GetProfileByIdQuery query);
        Task<UserProfile?> Handle(GetProfileByUserIdQuery query);
    }
}
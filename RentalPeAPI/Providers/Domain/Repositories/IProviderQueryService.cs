using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Model.Queries;

namespace RentalPeAPI.providers.Domain.Repositories;

public interface IProviderQueryService
{
    Task<Provider?> Handle(GetProviderByIdQuery query);
    Task<IEnumerable<Provider>> Handle(GetAllProvidersQuery query);
}

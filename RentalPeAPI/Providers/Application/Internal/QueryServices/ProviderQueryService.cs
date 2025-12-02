using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Model.Queries;
using RentalPeAPI.providers.Domain.Repositories;
using RentalPeAPI.providers.Domain.Services;

namespace RentalPeAPI.providers.Application.Internal.QueryServices;

public class ProviderQueryService : IProviderQueryService
{
    private readonly IProviderRepository _repository;

    public ProviderQueryService(IProviderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Provider?> Handle(GetProviderByIdQuery query)
    {
        return await _repository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Provider>> Handle(GetAllProvidersQuery query)
    {
        return await _repository.FindAllAsync();
    }
}
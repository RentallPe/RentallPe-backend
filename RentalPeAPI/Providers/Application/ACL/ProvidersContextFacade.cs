using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Model.Commands;
using RentalPeAPI.providers.Domain.Model.Queries;
using RentalPeAPI.providers.Domain.Repositories;
using RentalPeAPI.providers.Domain.Services;

namespace RentalPeAPI.Providers.Application.ACL
{
    public class ProvidersContextFacade
    {
        private readonly IProviderCommandService _commandService;
        private readonly IProviderQueryService _queryService;

        public ProvidersContextFacade(
            IProviderCommandService commandService,
            IProviderQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        // Commands
        public Task<Provider> CreateProvider(CreateProviderCommand command)
            => _commandService.Handle(command);

        public Task<Provider> UpdateProvider(UpdateProviderCommand command)
            => _commandService.Handle(command);

        public Task<bool> DeleteProvider(DeleteProviderCommand command)
            => _commandService.Handle(command);

        // Queries
        public Task<Provider?> GetProviderById(GetProviderByIdQuery query)
            => _queryService.Handle(query);

        public Task<IEnumerable<Provider>> GetAllProviders()
            => _queryService.Handle(new GetAllProvidersQuery());
    }
}
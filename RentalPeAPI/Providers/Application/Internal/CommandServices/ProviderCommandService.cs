using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Model.Commands;
using RentalPeAPI.providers.Domain.Repositories;
using RentalPeAPI.providers.Domain.Services;

namespace RentalPeAPI.Providers.Application.Internal.CommandServices
{
    public class ProviderCommandService : IProviderCommandService
    {
        private readonly IProviderRepository _repository;

        public ProviderCommandService(IProviderRepository repository)
        {
            _repository = repository;
        }

        // ✅ CREATE
        public async Task<Provider> Handle(CreateProviderCommand command)
        {
            var provider = new Provider(command.Name, command.ContactEmail);

            await _repository.AddAsync(provider);
            return provider;
        }

        // ✅ UPDATE
        public async Task<Provider> Handle(UpdateProviderCommand command)
        {
            var provider = await _repository.FindByIdAsync(command.Id)
                           ?? throw new Exception("Provider not found");

            provider.Update(command.Name, command.ContactEmail);

            await _repository.UpdateAsync(provider);
            return provider;
        }

        // ✅ DELETE
        public async Task<bool> Handle(DeleteProviderCommand command)
        {
            var provider = await _repository.FindByIdAsync(command.Id);
            if (provider == null) return false;

            await _repository.DeleteAsync(provider);
            return true;
        }
    }
}
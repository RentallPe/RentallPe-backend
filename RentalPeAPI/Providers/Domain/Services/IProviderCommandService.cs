namespace RentalPeAPI.providers.Domain.Services;

using RentalPeAPI.providers.Domain.Model.Aggregates;
using RentalPeAPI.providers.Domain.Model.Commands;

public interface IProviderCommandService
{
    Task<Provider> Handle(CreateProviderCommand command);
    Task<Provider> Handle(UpdateProviderCommand command);
    Task<bool> Handle(DeleteProviderCommand command);
}
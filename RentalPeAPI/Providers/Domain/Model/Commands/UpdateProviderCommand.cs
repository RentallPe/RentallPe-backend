
namespace RentalPeAPI.providers.Domain.Model.Commands;

public sealed record UpdateProviderCommand(int Id, string Name, string ContactEmail);

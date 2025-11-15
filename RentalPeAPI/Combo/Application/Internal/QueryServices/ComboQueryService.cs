using RentalPeAPI.Combo.Application.Internal.Dtos;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
namespace RentalPeAPI.Combo.Application.Internal.QueryServices
{
    public class ComboQueryService
    {
        private readonly IComboRepository _repository;

        public ComboQueryService(IComboRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ComboDto>> ListAsync(Guid? providerId = null)
        {
            var combos = await _repository.ListAsync(providerId);
            return combos.Select(ComboDto.FromDomain);
        }

        public async Task<ComboDto?> GetByIdAsync(int id)
        {
            var combo = await _repository.FindByIdAsync(id); // 👈 aquí va 'id'
            return combo == null ? null : ComboDto.FromDomain(combo);
        }
    }
}
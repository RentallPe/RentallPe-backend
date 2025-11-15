using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.Dtos;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories;
using RentalPeAPI.Combo.Domain.Aggregates.Entities;
namespace RentalPeAPI.Combo.Application.Services;

public class ComboAppService
{
    private readonly IComboRepository _comboRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ComboAppService(IComboRepository comboRepository, IUnitOfWork unitOfWork)
    {
        _comboRepository = comboRepository;
        _unitOfWork = unitOfWork;
    }

    // Crear Combo
    public async Task<ComboDto> CreateComboAsync(CreateComboCommand command)
    {
        var combo = new Domain.Aggregates.Entities.Combo(
            command.Name,
            command.Description,
            command.Price,
            command.InstallDays,
            command.Image,
            command.ProviderId
        );

        await _comboRepository.AddAsync(combo);
        await _unitOfWork.CompleteAsync();

        return ComboDto.FromDomain(combo);
    }

    // Actualizar Combo
    public async Task<ComboDto?> UpdateComboAsync(UpdateComboCommand command)
    {
        var combo = await _comboRepository.FindByIdAsync(command.Id);
        if (combo == null) return null;

        combo.Update(
            command.Name,
            command.Description,
            command.Price,
            command.InstallDays,
            command.Image,
            command.ProviderId
        );

        await _unitOfWork.CompleteAsync();
        return ComboDto.FromDomain(combo);
    }

    // Eliminar Combo
    public async Task<bool> DeleteComboAsync(DeleteComboCommand command)
    {
        var combo = await _comboRepository.FindByIdAsync(command.Id);
        if (combo == null) return false;

        _comboRepository.Remove(combo);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    // Buscar por Id
    public async Task<ComboDto?> GetComboByIdAsync(GetComboByIdQuery query)
    {
        var combo = await _comboRepository.FindByIdAsync(query.Id);
        return combo != null ? ComboDto.FromDomain(combo) : null;
    }

    // Listar combos (opcionalmente filtrados por ProviderId)
    public async Task<IEnumerable<ComboDto>> ListCombosAsync(ListCombosQuery query)
    {
        var combos = await _comboRepository.ListAsync(query.ProviderId);
        return combos.Select(ComboDto.FromDomain).ToList();
    }
}
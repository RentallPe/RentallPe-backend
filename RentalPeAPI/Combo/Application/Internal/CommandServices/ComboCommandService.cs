using RentalPeAPI.Combo.Application.Internal.Dtos;
using RentalPeAPI.Combo.Domain.Aggregates.Entities;
using RentalPeAPI.Combo.Domain.Repositories;
using RentalPeAPI.Combo.Infrastructure.Persistence;
using RentalPeAPI.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace RentalPeAPI.Combo.Application.Internal.CommandServices;

public class ComboCommandService
{
    private readonly IComboRepository _repository;
    private readonly AppDbContext _context; // ← reemplazado

    public ComboCommandService(IComboRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    // ========== CREATE ==========
    public async Task<ComboDto> HandleAsync(CreateComboCommand command)
    {
        var combo = new Domain.Aggregates.Entities.Combo(
            command.Name,
            command.Description,
            command.Price,
            command.InstallDays,
            command.Image,
            command.ProviderId,
            command.PlanType
        );

        await _repository.AddAsync(combo);
        await _context.SaveChangesAsync();

        return ComboDto.FromDomain(combo);
    }

    // ========== UPDATE ==========
    public async Task<ComboDto?> HandleUpdateAsync(UpdateComboCommand command)
    {
        var combo = await _repository.FindByIdAsync(command.Id);
        if (combo == null) return null;

        combo.Update(
            command.Name,
            command.Description,
            command.Price,
            command.InstallDays,
            command.Image,
            command.ProviderId,
            command.PlanType // 👈 ahora se pasa al método Update
        );

        await _context.SaveChangesAsync();
        return ComboDto.FromDomain(combo);
    }

    // ========== DELETE ==========
    public async Task<bool> HandleDeleteAsync(int id)
    {
        var combo = await _repository.FindByIdAsync(id);
        if (combo == null) return false;

        _repository.Remove(combo);
        await _context.SaveChangesAsync();

        return true;
    }
}
using RentalPeAPI.Property.Application.Internal.CommandServices;
using RentalPeAPI.Property.Application.Internal.Dtos;
using RentalPeAPI.Property.Application.Internal.QueryServices;
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Repositories;
using RentalPeAPI.Shared.Domain.Repositories; // <-- ¡Asegúrate de tener este using!

namespace RentalPeAPI.Property.Application.Services;

public class SpaceAppService
{
    private readonly ISpaceRepository _spaceRepository;
    private readonly IUnitOfWork _unitOfWork; // <-- 1. Añade el Unit of Work

    // 2. Inyecta el IUnitOfWork en el constructor
    public SpaceAppService(ISpaceRepository spaceRepository, IUnitOfWork unitOfWork)
    {
        _spaceRepository = spaceRepository;
        _unitOfWork = unitOfWork; 
    }

    public async Task<SpaceDto> CreateSpaceAsync(CreateSpaceCommand command)
    {
        var space = new Space(
            command.Name,
            command.Description,
            command.PricePerHour,
            command.Type,
            command.Location,
            command.OwnerId,
            command.Services
        );

        await _spaceRepository.AddAsync(space);
        
        // --- ¡ARREGLO! Usa el Unit of Work ---
        await _unitOfWork.CompleteAsync(); 

        return ToDto(space);
    }


    public async Task<SpaceDto?> UpdateSpaceAsync(UpdateSpaceCommand command)
    {
        var space = await _spaceRepository.FindByIdAsync(command.Id);
        if (space == null) return null;

        space.Update(
            command.Name,
            command.Description,
            command.PricePerHour,
            command.Type,
            command.Location,
            command.Services,
            command.AreaM2,
            command.Status
        );

        // --- ¡ARREGLO! Usa el Unit of Work ---
        await _unitOfWork.CompleteAsync(); 

        return ToDto(space);
    }

    public async Task<bool> DeleteSpaceAsync(DeleteSpaceCommand command)
    {
        var space = await _spaceRepository.FindByIdAsync(command.Id);
        if (space == null) return false;

        _spaceRepository.Remove(space);
        
        // --- ¡ARREGLO! Usa el Unit of Work ---
        await _unitOfWork.CompleteAsync(); 
        return true;
    }

    // --- Los métodos GET no cambian (no guardan nada) ---

    public async Task<SpaceDto?> GetSpaceByIdAsync(GetSpaceByIdQuery query)
    {
        var space = await _spaceRepository.FindByIdAsync(query.Id);
        return space != null ? ToDto(space) : null;
    }

    public async Task<IEnumerable<SpaceDto>> ListSpacesAsync(ListSpacesQuery query)
    {
        var spaces = await _spaceRepository.ListAsync(query.OwnerId, query.Type);
        return spaces.Select(ToDto).ToList();
    }

    private static SpaceDto ToDto(Space space)
    {
        return new SpaceDto
        {
            Id = space.Id,
            Name = space.Name,
            Description = space.Description,
            PricePerHour = space.PricePerHour,
            Type = space.Type.ToString(), 
            Location = space.Location?.Address ?? string.Empty, 
            OwnerId = space.OwnerId.Value, 
            Services = space.Services.Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList(),
            Status = space.Status,
            AreaM2 = space.AreaM2,
            CreatedAt = space.CreatedAt
        };
    }
}
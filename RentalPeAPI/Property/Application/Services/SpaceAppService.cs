using RentalPeAPI.Property.Application.Internal.CommandServices;
using RentalPeAPI.Property.Application.Internal.Dtos;
using RentalPeAPI.Property.Application.Internal.QueryServices;
using RentalPeAPI.Property.Domain.Aggregates;
using RentalPeAPI.Property.Domain.Repositories;

namespace RentalPeAPI.Property.Application.Services;

public class SpaceAppService
{
    private readonly ISpaceRepository _spaceRepository;

    public SpaceAppService(ISpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
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
        await _spaceRepository.SaveChangesAsync();

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

        await _spaceRepository.SaveChangesAsync();

        return ToDto(space);
    }

    public async Task<bool> DeleteSpaceAsync(DeleteSpaceCommand command)
    {
        var space = await _spaceRepository.FindByIdAsync(command.Id);
        if (space == null) return false;

        _spaceRepository.Remove(space);
        await _spaceRepository.SaveChangesAsync();
        return true;
    }

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
            Type = space.Type.ToString(), // Enum a string
            Location = space.Location?.Address ?? string.Empty, // tu ValueObject Location
            OwnerId = space.OwnerId.Value, // OwnerId es un VO
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

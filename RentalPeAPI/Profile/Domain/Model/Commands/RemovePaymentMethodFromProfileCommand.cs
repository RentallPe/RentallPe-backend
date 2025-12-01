namespace RentalPeAPI.Profile.Domain.Model.Commands;

/// <summary>Consulta para obtener un perfil por su Id.</summary>
public record GetProfileByIdQuery(int ProfileId);
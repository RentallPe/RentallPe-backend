namespace RentalPeAPI.Profile.Domain.Model.Queries;

/// <summary>Consulta para obtener un perfil por Id de usuario.</summary>
public record GetProfileByUserIdQuery(long UserId);
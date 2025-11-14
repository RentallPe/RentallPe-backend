namespace RentalPeAPI.Profile.Domain.Model.Commands;

/// <summary>Comando para actualizar los datos básicos del perfil.</summary>
public record UpdateProfileCommand(
    int ProfileId,
    string FullName,
    string Country,
    string Department,
    string PrimaryEmail,
    string PrimaryPhone);
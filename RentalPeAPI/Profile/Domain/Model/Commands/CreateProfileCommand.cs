namespace RentalPeAPI.Profile.Domain.Model.Commands;

/// <summary>Comando para crear un perfil.</summary>
public record CreateProfileCommand(
    long UserId,
    string FullName,
    string Country,
    string Department,
    string PrimaryEmail,
    string PrimaryPhone);
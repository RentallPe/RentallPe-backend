namespace RentalPeAPI.Profile.Domain.Model.ValueObjects
{
    /// <summary>
    /// Value object que representa el identificador del usuario (IAM / User BC).
    /// </summary>
    /// <param name="Value">Valor del identificador de usuario.</param>
    public record UserId(long Value);
}
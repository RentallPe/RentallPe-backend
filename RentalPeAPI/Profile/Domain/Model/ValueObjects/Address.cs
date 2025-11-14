namespace RentalPeAPI.Profile.Domain.Model.ValueObjects
{
    /// <summary>
    /// Value object que representa una dirección principal del usuario.
    /// </summary>
    public record Address(
        string Line1,
        string? Line2,
        string? District,
        string City,
        string? State,
        string? PostalCode,
        string Country);
}
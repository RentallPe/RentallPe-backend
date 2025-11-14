namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

/// <summary>
///     Address resource usado dentro de las respuestas de Profile.
/// </summary>
/// <param name="Line1">Línea principal de dirección.</param>
/// <param name="Line2">Segunda línea opcional.</param>
/// <param name="District">Distrito o barrio.</param>
/// <param name="City">Nombre de la ciudad.</param>
/// <param name="State">Estado o región.</param>
/// <param name="PostalCode">Código postal / ZIP.</param>
/// <param name="Country">Nombre del país.</param>
public record AddressResource(
    string Line1,
    string? Line2,
    string? District,
    string City,
    string? State,
    string? PostalCode,
    string Country);
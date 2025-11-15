namespace RentalPeAPI.User.Application.Internal.CommandServices;

public record UserDto(
    Guid Id,
    string FullName,
    string Email,
    string? Phone, // NUE 2025-11-15 Braulio
    DateTime CreatedAt, // NUE 2025-11-15 Braulio
    string Role, // NUE 2025-11-15 Braulio
    Guid? ProviderId, // NUE 2025-11-15 Braulio
    string? Photo // NUE 2025-11-15 Braulio
);
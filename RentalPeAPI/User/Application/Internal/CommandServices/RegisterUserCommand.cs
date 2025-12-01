using MediatR;

namespace RentalPeAPI.User.Application.Internal.CommandServices;

public record RegisterUserCommand(
    string FullName,
    string Email,
    string Password,
    string? Phone, // NUE 2025-11-15 Braulio
    string Role = "customer", // NUE 2025-11-15 Braulio
    Guid? ProviderId = null, // NUE 2025-11-15 Braulio
    string? Photo = null // NUE 2025-11-15 Braulio

) : IRequest<UserDto>;
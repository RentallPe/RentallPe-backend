using MediatR;
using System; // Para Guid
using System.Threading; // Para CancellationToken

// --- USINGS FALTANTES Y ESENCIALES ---
using RentalPeAPI.User.Domain; // <-- ¡Para la entidad AppUser! (Faltaba)
using RentalPeAPI.User.Application.Internal.CommandServices; // <-- Para UserDto y RegisterUserCommand

using RentalPeAPI.User.Domain.Services; // Para IPasswordHashingService
// --- FIN USINGS ESENCIALES ---

// --- 1. SOLUCIÓN: ALIAS para IUnitOfWork COMPARTIDO (Se mantiene) ---
using SharedIUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;

// --- 2. ELIMINAMOS la línea 'using RentalPeAPI.User.Domain.Repositories;' para evitar conflicto ---
//    Y usamos la ruta completa para IUserRepository, que es la única clase que necesitamos.

namespace RentalPeAPI.User.Application.Internal.CommandServices;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    // Usamos la ruta completa para IUserRepository (Reemplaza el 'using' eliminado)
    private readonly RentalPeAPI.User.Domain.Repositories.IUserRepository _userRepository; 
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly SharedIUnitOfWork _unitOfWork; 

    public RegisterUserCommandHandler(
        // Usamos la ruta completa para IUserRepository:
        RentalPeAPI.User.Domain.Repositories.IUserRepository userRepository, 
        IPasswordHashingService passwordHashingService, 
        // Usamos el alias para el UnitOfWork:
        SharedIUnitOfWork unitOfWork) 
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        // ... (Tu lógica de validación e hashing)

        if (await _userRepository.UserExistsByEmailAsync(command.Email))
        {
            throw new Exception("El correo electrónico ya está en uso."); 
        }

        var hashedPassword = _passwordHashingService.HashPassword(command.Password);

        var user = new Domain.AppUser(
            Guid.NewGuid(),
            command.FullName,
            command.Email,
            hashedPassword,
            command.Phone, // NUE 2025-11-15 Braulio
            command.Role, // NUE 2025-11-15 Braulio
            command.ProviderId, // NUE 2025-11-15 Braulio
            command.Photo // NUE 2025-11-15 Braulio

        );

        await _userRepository.AddAsync(user);
        
        await _unitOfWork.CompleteAsync(); // Usando el IUnitOfWork compartido

        return new UserDto(
            user.Id,
            user.FullName,
            user.Email,user.Phone, // NUE 2025-11-15 Braulio
            user.CreatedAt, // NUE 2025-11-15 Braulio
            user.Role, // NUE 2025-11-15 Braulio
            user.ProviderId, // NUE 2025-11-15 Braulio
            user.Photo // NUE 2025-11-15 Braulio
        );
    }
}
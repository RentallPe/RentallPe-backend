using MediatR;
using RentalPeAPI.User.Domain.Repositories; // Necesario para IUserRepository
using RentalPeAPI.User.Domain.Services;
using RentalPeAPI.User.Domain; 


using SharedIUnitOfWork = RentalPeAPI.Shared.Domain.Repositories.IUnitOfWork;

namespace RentalPeAPI.User.Application.Internal.CommandServices;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly SharedIUnitOfWork _unitOfWork; // 2. Usamos el alias aquí

    public RegisterUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHashingService passwordHashingService, 
        SharedIUnitOfWork unitOfWork) // 3. Usamos el alias aquí
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
            hashedPassword
        );

        await _userRepository.AddAsync(user);
        
        // El Unit of Work es el compartido
        await _unitOfWork.CompleteAsync(); // Asumo que el método correcto es CompleteAsync()

      
        return new UserDto(user.Id, user.FullName, user.Email);
    }
}
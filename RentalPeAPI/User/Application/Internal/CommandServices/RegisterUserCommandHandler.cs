using MediatR;
using RentalPeAPI.User.Domain.Repositories;
using RentalPeAPI.User.Domain.Services;
using RentalPeAPI.User.Domain; 

namespace RentalPeAPI.User.Application.Internal.CommandServices;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHashingService passwordHashingService, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
      
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
        
       
        await _unitOfWork.SaveChangesAsync(cancellationToken);

      
        return new UserDto(user.Id, user.FullName, user.Email);
    }
}
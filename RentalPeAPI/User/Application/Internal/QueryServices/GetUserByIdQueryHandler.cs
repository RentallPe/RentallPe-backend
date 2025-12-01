using MediatR;
using RentalPeAPI.User.Application.Internal.CommandServices;
using RentalPeAPI.User.Domain.Repositories;

namespace RentalPeAPI.User.Application.Internal.QueryServices;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
      
        var user = await _userRepository.GetByIdAsync(query.UserId);

       
        if (user is null)
        {
            return null;
        }


        return new UserDto(user.Id,
            user.FullName,
            user.Email, user.Phone, // NUE 2025-11-15 Braulio
            user.CreatedAt, // NUE 2025-11-15 Braulio
            user.Role, // NUE 2025-11-15 Braulio
            user.ProviderId, // NUE 2025-11-15 Braulio
            user.Photo // NUE 2025-11-15 Braulio);
        );
    }
}
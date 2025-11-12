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

        
        return new UserDto(user.Id, user.FullName, user.Email);
    }
}
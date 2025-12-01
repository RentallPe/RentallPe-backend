using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Domain.Model.Commands;
using RentalPeAPI.Profiles.Domain.Repositories;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork)
    : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);

        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception e)
        {
            // Log error
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<PaymentMethod?> Handle(AddPaymentMethodCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            var paymentMethod = profile.AddPaymentMethod(
                command.Type,
                command.Number,
                command.Expiry,
                command.Cvv);

            await unitOfWork.CompleteAsync();
            return paymentMethod;
        }
        catch (Exception e)
        {
            // Log error
            Console.WriteLine(e);
            return null;
        }
    }
}
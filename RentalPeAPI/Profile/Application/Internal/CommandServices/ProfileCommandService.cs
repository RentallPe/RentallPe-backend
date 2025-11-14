using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

// Alias para dejar claro que usamos el agregado de dominio
using ProfileAggregate = RentalPeAPI.Profile.Domain.Model.Aggregates.Profile;

namespace RentalPeAPI.Profile.Application.Internal.CommandServices;

/// <summary>
/// Implements the profile command service for the RentalPe platform.
/// </summary>
public class ProfileCommandService : IProfileCommandService
{
    private readonly IProfileRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileCommandService(
        IProfileRepository profileRepository,
        IUnitOfWork unitOfWork)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the create profile command.
    /// </summary>
    /// <param name="command">The <see cref="CreateProfileCommand"/> to handle.</param>
    /// <returns>The created <see cref="ProfileAggregate"/> or null if it already exists.</returns>
    public async Task<ProfileAggregate?> Handle(CreateProfileCommand command)
    {
        // Evitar duplicar perfiles por UserId
        var existingProfile = await _profileRepository.FindByUserIdAsync(command.UserId);
        if (existingProfile is not null) return null;

        var profile = new ProfileAggregate(
            command.UserId,
            command.FullName,
            command.Country,
            command.Department,
            command.PrimaryEmail,
            command.PrimaryPhone);

        await _profileRepository.AddAsync(profile);
        await _unitOfWork.CompleteAsync();

        return profile;
    }

    /// <summary>
    /// Handles the update profile command.
    /// </summary>
    /// <param name="command">The <see cref="UpdateProfileCommand"/> to handle.</param>
    /// <returns>The updated <see cref="ProfileAggregate"/> or null if it was not found.</returns>
    public async Task<ProfileAggregate?> Handle(UpdateProfileCommand command)
    {
        var profile = await _profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        profile.UpdateBasicInformation(
            command.FullName,
            command.Country,
            command.Department,
            command.PrimaryEmail,
            command.PrimaryPhone);

        _profileRepository.Update(profile);
        await _unitOfWork.CompleteAsync();

        return profile;
    }

    /// <summary>
    /// Handles the add payment method to profile command.
    /// </summary>
    public async Task<ProfileAggregate?> Handle(AddPaymentMethodToProfileCommand command)
    {
        var profile = await _profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        profile.AddPaymentMethod(command.PaymentMethod);
        _profileRepository.Update(profile);
        await _unitOfWork.CompleteAsync();

        return profile;
    }

    /// <summary>
    /// Handles the remove payment method from profile command.
    /// </summary>
    public async Task<ProfileAggregate?> Handle(RemovePaymentMethodFromProfileCommand command)
    {
        var profile = await _profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        profile.RemovePaymentMethod(command.PaymentMethodId);
        _profileRepository.Update(profile);
        await _unitOfWork.CompleteAsync();

        return profile;
    }
}

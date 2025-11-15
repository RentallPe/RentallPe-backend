using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profile.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Domain.Model.Aggregates.Profile?> Handle(CreateProfileCommand command)
    {
        var byUser = await profileRepository.FindByUserIdAsync(command.UserId.Value);
        if (byUser is not null) return null;

        var byEmail = await profileRepository.FindByEmailAsync(command.PrimaryEmail);
        if (byEmail is not null) return null;

        var profile = new Domain.Model.Aggregates.Profile(
            command.UserId,
            command.FullName,
            command.PrimaryEmail,
            command.Avatar,
            command.Bio,
            command.PrimaryPhone,
            command.PrimaryAddress);

        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileNameCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            profile.UpdateName(command.FullName);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileBioCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            profile.UpdateBio(command.Bio);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileEmailCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;
        
        var byEmail = await profileRepository.FindByEmailAsync(command.Email);
        if (byEmail is not null && byEmail.Id != profile.Id) return null;

        try
        {
            profile.UpdateEmail(command.Email);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfilePhoneCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            profile.UpdatePhone(command.Phone);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileAddressCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            profile.UpdateAddress(command.Address);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Model.Aggregates.Profile?> Handle(UpdateProfileAvatarCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile is null) return null;

        try
        {
            profile.UpdateAvatar(command.Avatar);
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch
        {
            return null;
        }
    }
}
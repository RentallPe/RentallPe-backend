using RentalPeAPI.Profile.Domain.Model.Aggregates;
using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Repositories;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Profile.Application.Internal.CommandServices;

public class PreferenceSetCommandService(
    IPreferenceSetRepository preferenceRepository,
    IUnitOfWork unitOfWork) : IPreferenceSetCommandService
{
    public async Task<PreferenceSet?> Handle(CreatePreferenceSetCommand command)
    {
        var existing = await preferenceRepository.FindByUserIdAsync(command.UserId.Value);
        if (existing is not null) return null;

        var pref = new PreferenceSet(
            command.UserId,
            command.Language,
            command.Theme,
            command.TimeZone,
            command.Notifications,
            command.Privacy,
            command.QuietHours);

        try
        {
            await preferenceRepository.AddAsync(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(UpdatePreferenceLanguageCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.UpdateLanguage(command.Language);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(UpdatePreferenceThemeCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.UpdateTheme(command.Theme);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(UpdatePreferenceTimeZoneCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.UpdateTimeZone(command.TimeZone);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(SetPreferenceNotificationsCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.SetNotifications(command.Notifications);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(SetPreferencePrivacyCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.SetPrivacy(command.Privacy);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(SetQuietHoursCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.SetQuietHours(command.QuietHours);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(ClearQuietHoursCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.ClearQuietHours();
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(AddFavoriteCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.AddFavorite(command.RemodelingId);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }

    public async Task<PreferenceSet?> Handle(RemoveFavoriteCommand command)
    {
        var pref = await preferenceRepository.FindByIdAsync(command.PreferenceSetId);
        if (pref is null) return null;

        try
        {
            pref.RemoveFavorite(command.RemodelingId);
            preferenceRepository.Update(pref);
            await unitOfWork.CompleteAsync();
            return pref;
        }
        catch
        {
            return null;
        }
    }
}
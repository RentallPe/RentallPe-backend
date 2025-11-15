using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Domain.Model.Commands;


public sealed record UpdatePreferenceLanguageCommand(int PreferenceSetId, LanguageCode Language);
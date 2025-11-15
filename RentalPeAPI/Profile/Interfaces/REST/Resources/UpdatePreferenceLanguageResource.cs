using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record UpdatePreferenceLanguageResource(LanguageCode Language);
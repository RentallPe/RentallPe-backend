using RentalPeAPI.Profile.Domain.Model.Enums;

namespace RentalPeAPI.Profile.Domain.Model.Queries;


public record GetPreferenceSetsByLanguageQuery(LanguageCode Language);
using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record UpdatePreferenceTimeZoneResource([Required, MaxLength(100)] string TimeZone);
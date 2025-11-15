using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record PhoneResource(
    [Required, RegularExpression(@"^\+?[1-9]\d{6,14}$")]
    string Number
);
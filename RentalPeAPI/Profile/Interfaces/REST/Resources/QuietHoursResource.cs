using System.ComponentModel.DataAnnotations;

namespace RentalPeAPI.Profile.Interfaces.REST.Resources;

public record QuietHoursResource(
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$")] string Start,
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$")] string End
);
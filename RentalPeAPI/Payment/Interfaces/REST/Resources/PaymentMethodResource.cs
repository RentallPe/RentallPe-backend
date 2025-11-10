using System.ComponentModel.DataAnnotations;
using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Interfaces.REST.Resources;

public record PaymentMethodResource(
    [Required] PaymentMethodType Type,
    string? Label,
    [StringLength(4, MinimumLength = 4)] string? Last4);
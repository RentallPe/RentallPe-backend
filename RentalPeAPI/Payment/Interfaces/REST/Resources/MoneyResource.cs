using System.ComponentModel.DataAnnotations;
using RentalPeAPI.Payment.Domain.Model.Enums;

namespace RentalPeAPI.Payment.Interfaces.REST.Resources;

public record MoneyResource(
    [Range(0, double.MaxValue)] decimal Amount,
    [Required] Currency Currency);
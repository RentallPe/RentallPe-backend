using System.ComponentModel.DataAnnotations;
using RentalPeAPI.Payments.Domain.Model.Enums;

namespace RentalPeAPI.Payments.Interfaces.REST.Resources.payments;

public record CreatePaymentResource(
    [Required] int UserId,
    [Required] int ProjectId,
    [Required] int Installment,

    [Range(0, double.MaxValue)] decimal Amount,
    [Required] Currency Currency,

    DateTimeOffset? Date);
using System.ComponentModel.DataAnnotations;
using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Interfaces.REST.Resources;

public record CreateSubscriptionResource(
    [Required] int CustomerId,
    [Required] SubscriptionPlan Plan,
    [Range(0, double.MaxValue)] decimal Price,
    [Required] DateTimeOffset StartDate,
    [Required] DateTimeOffset EndDate);
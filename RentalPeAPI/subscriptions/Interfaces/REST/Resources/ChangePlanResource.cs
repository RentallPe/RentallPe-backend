using System.ComponentModel.DataAnnotations;
using RentalPeAPI.subscriptions.Domain.Model.Enums;

namespace RentalPeAPI.subscriptions.Interfaces.REST.Resources;

public record ChangePlanResource(
    [Required] SubscriptionPlan NewPlan,
    [Range(0, double.MaxValue)] decimal NewPrice);
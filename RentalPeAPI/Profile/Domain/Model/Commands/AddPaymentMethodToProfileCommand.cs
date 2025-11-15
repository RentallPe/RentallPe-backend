using RentalPeAPI.Profile.Domain.Model.Entities;

namespace RentalPeAPI.Profile.Domain.Model.Commands;

/// <summary>Comando para agregar un método de pago al perfil.</summary>
public record AddPaymentMethodToProfileCommand(
    int ProfileId,
    PaymentMethod PaymentMethod);
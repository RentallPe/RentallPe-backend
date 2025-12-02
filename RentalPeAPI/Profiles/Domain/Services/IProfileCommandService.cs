using RentalPeAPI.Profiles.Domain.Model.Aggregates;
using RentalPeAPI.Profiles.Domain.Model.Commands;

namespace RentalPeAPI.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);

    Task<PaymentMethod?> Handle(AddPaymentMethodCommand command);
}
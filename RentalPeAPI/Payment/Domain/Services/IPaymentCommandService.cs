using RentalPeAPI.Payment.Domain.Model.Commands.payments;

namespace RentalPeAPI.Payment.Domain.Services;

public interface IPaymentCommandService
{
    Task<Model.Aggregates.Payment?> Handle(CreatePaymentCommand command);
    Task<Model.Aggregates.Payment?> Handle(InitiatePaymentCommand command);
    Task<Model.Aggregates.Payment?> Handle(ConfirmPaymentCommand command);
    Task<Model.Aggregates.Payment?> Handle(CancelPaymentCommand command);
    Task<Model.Aggregates.Payment?> Handle(RefundPaymentCommand command);
}
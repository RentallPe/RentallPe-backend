using RentalPeAPI.Payment.Domain.Model.Queries.Payments;
using RentalPeAPI.Payment.Domain.Repositories;
using RentalPeAPI.Payment.Domain.Services;

namespace RentalPeAPI.Payment.Application.Internal.QueryServices;

public class PaymentQueryService(IPaymentRepository paymentRepository) : IPaymentQueryService
{
    public async Task<Domain.Model.Aggregates.Payment?> Handle(GetPaymentByIdQuery query)
        => await paymentRepository.FindByIdAsync(query.Id);

    public async Task<IEnumerable<Domain.Model.Aggregates.Payment>> Handle(GetPaymentsByUserIdQuery query)
        => await paymentRepository.FindByUserIdAsync(query.UserId);

    public async Task<Domain.Model.Aggregates.Payment?> Handle(GetPaymentByReferenceQuery query)
        => await paymentRepository.FindByReferenceAsync(query.Reference);

    public async Task<IEnumerable<Domain.Model.Aggregates.Payment>> Handle(GetPaymentsByStatusQuery query)
        => query.UserId.HasValue
            ? await paymentRepository.FindByStatusAndUserIdAsync(query.Status, query.UserId.Value)
            : await paymentRepository.FindByStatusAsync(query.Status);
}
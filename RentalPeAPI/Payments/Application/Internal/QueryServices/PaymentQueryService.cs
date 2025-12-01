using RentalPeAPI.Payments.Domain.Model.Queries.Payments;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services;

namespace RentalPeAPI.Payments.Application.Internal.QueryServices;

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
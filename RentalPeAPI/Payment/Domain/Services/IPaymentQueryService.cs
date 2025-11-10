using RentalPeAPI.Payment.Domain.Model.Queries.Payments;

namespace RentalPeAPI.Payment.Domain.Services;

public interface IPaymentQueryService
{
    Task<Model.Aggregates.Payment?> Handle(GetPaymentByIdQuery query);
    Task<IEnumerable<Model.Aggregates.Payment>> Handle(GetPaymentsByUserIdQuery query);
    Task<Model.Aggregates.Payment?> Handle(GetPaymentByReferenceQuery query);
    Task<IEnumerable<Model.Aggregates.Payment>> Handle(GetPaymentsByStatusQuery query);
}
using RentalPeAPI.Payments.Domain.Model.Queries.Payments;

namespace RentalPeAPI.Payments.Domain.Services;

public interface IPaymentQueryService
{
    Task<Model.Aggregates.Payment?> Handle(GetPaymentByIdQuery query);
    Task<IEnumerable<Model.Aggregates.Payment>> Handle(GetPaymentsByUserIdQuery query);
    Task<Model.Aggregates.Payment?> Handle(GetPaymentByReferenceQuery query);
    Task<IEnumerable<Model.Aggregates.Payment>> Handle(GetPaymentsByStatusQuery query);
}
using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payments.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> FindByUserIdAsync(int userId);
    Task<Payment?> FindByReferenceAsync(string reference);
    Task<IEnumerable<Model.Aggregates.Payment>> FindByStatusAsync(PaymentStatus status);
    Task<IEnumerable<Model.Aggregates.Payment>> FindByStatusAndUserIdAsync(PaymentStatus status, int userId);
}
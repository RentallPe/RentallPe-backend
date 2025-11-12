using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payment.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Model.Aggregates.Payment>
{
    Task<IEnumerable<Model.Aggregates.Payment>> FindByUserIdAsync(int userId);
    Task<Model.Aggregates.Payment?> FindByReferenceAsync(string reference);
    Task<IEnumerable<Model.Aggregates.Payment>> FindByStatusAsync(PaymentStatus status);
    Task<IEnumerable<Model.Aggregates.Payment>> FindByStatusAndUserIdAsync(PaymentStatus status, int userId);
}
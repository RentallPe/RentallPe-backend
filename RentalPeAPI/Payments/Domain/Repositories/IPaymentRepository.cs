using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payments.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> FindByUserIdAsync(int userId);

    Task<Payment?> FindByReferenceAsync(string reference);

    Task<IEnumerable<Payment>> FindByStatusAsync(PaymentStatus status);

    Task<IEnumerable<Payment>> FindByStatusAndUserIdAsync(PaymentStatus status, int userId);
    
    Task<IEnumerable<Payment>> FindByProjectIdAsync(int projectId);

    Task<Payment?> FindByProjectAndInstallmentAsync(int projectId, int installment);
}
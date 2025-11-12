using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Interfaces.REST.Resources;

namespace RentalPeAPI.Payment.Interfaces.REST.Transform;

public static class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice entity)
        => new(entity.Id,
            entity.PaymentId,
            entity.BookingId,
            entity.UserId,
            entity.IssueDate,
            new MoneyResource(entity.Total.Amount, entity.Total.Currency),
            entity.Status,
            entity.CreatedDate);
}
using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Interfaces.REST.Resources.invoices;

namespace RentalPeAPI.Payments.Interfaces.REST.Transform;

public static class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice entity)
        => new(
            entity.Id,
            entity.PaymentId,
            entity.Number,
            entity.IssueDate,
            entity.CreatedDate);
}
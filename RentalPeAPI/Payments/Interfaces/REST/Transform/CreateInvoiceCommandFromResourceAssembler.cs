using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payments.Interfaces.REST.Resources.invoices;

namespace RentalPeAPI.Payments.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
        => new(
            resource.PaymentId,
            resource.Number,
            resource.IssueDate);
}
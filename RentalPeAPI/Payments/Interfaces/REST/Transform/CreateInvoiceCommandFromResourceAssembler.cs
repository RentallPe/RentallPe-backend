using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payments.Domain.Model.ValueObjects;
using RentalPeAPI.Payments.Interfaces.REST.Resources;

namespace RentalPeAPI.Payments.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
        => new(resource.PaymentId,
            resource.BookingId,
            resource.UserId,
            new Money(resource.Total.Amount, resource.Total.Currency));
}
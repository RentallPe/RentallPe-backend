using RentalPeAPI.Payment.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payment.Domain.Model.ValueObjects;
using RentalPeAPI.Payment.Interfaces.REST.Resources;

namespace RentalPeAPI.Payment.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
        => new(resource.PaymentId,
            resource.BookingId,
            resource.UserId,
            new Money(resource.Total.Amount, resource.Total.Currency));
}
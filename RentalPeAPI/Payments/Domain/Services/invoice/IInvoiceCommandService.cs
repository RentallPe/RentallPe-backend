using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;

namespace RentalPeAPI.Payments.Domain.Services.invoice;

public interface IInvoiceCommandService
{
    Task<Invoice?> Handle(CreateInvoiceCommand command);

    Task<Invoice?> Handle(IssueInvoiceCommand command);

    Task<Invoice?> Handle(SendInvoiceEmailCommand command);

    Task<Invoice?> Handle(VoidInvoiceCommand command);
}
using RentalPeAPI.Payment.Domain.Model.Aggregates;
using RentalPeAPI.Payment.Domain.Model.Commands.Invoices;

namespace RentalPeAPI.Payment.Domain.Services;

public interface IInvoiceCommandService
{
    Task<Invoice?> Handle(CreateInvoiceCommand command);
    Task<Invoice?> Handle(IssueInvoiceCommand command);
    Task<Invoice?> Handle(SendInvoiceEmailCommand command);
    Task<Invoice?> Handle(VoidInvoiceCommand command);
}
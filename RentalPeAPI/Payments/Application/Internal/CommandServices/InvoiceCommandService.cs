using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services.invoice;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payments.Application.Internal.CommandServices;

public class InvoiceCommandService(
    IInvoiceRepository invoiceRepository,
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : IInvoiceCommandService
{
    public async Task<Invoice?> Handle(CreateInvoiceCommand command)
    {
        // Validar que exista el Payment
        var payment = await paymentRepository.FindByIdAsync(command.PaymentId);
        if (payment is null) return null;

        // Asegurar 1 Invoice por Payment
        var existing = (await invoiceRepository.FindByPaymentIdAsync(command.PaymentId)).FirstOrDefault();
        if (existing is not null) return null;

        var invoice = new Invoice(
            paymentId: command.PaymentId,
            number: command.Number,
            issueDate: command.IssueDate
        );

        try
        {
            await invoiceRepository.AddAsync(invoice);
            await unitOfWork.CompleteAsync();
            return invoice;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Invoice?> Handle(IssueInvoiceCommand command)
    {
        var invoice = await invoiceRepository.FindByIdAsync(command.InvoiceId);
        if (invoice is null) return null;

        try
        {
            invoice.GeneratePdf();
            invoiceRepository.Update(invoice);
            await unitOfWork.CompleteAsync();
            return invoice;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Invoice?> Handle(SendInvoiceEmailCommand command)
    {
        var invoice = await invoiceRepository.FindByIdAsync(command.InvoiceId);
        if (invoice is null) return null;

        try
        {
            invoice.SendEmail();
            invoiceRepository.Update(invoice);
            await unitOfWork.CompleteAsync();
            return invoice;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Invoice?> Handle(VoidInvoiceCommand command)
    {
        var invoice = await invoiceRepository.FindByIdAsync(command.InvoiceId);
        if (invoice is null) return null;

        try
        {
            invoice.Void();
            invoiceRepository.Update(invoice);
            await unitOfWork.CompleteAsync();
            return invoice;
        }
        catch
        {
            return null;
        }
    }
}
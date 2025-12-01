using RentalPeAPI.Payments.Domain.Model.Aggregates;
using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services;
using RentalPeAPI.Shared.Domain.Repositories;

namespace RentalPeAPI.Payments.Application.Internal.CommandServices;

public class InvoiceCommandService(
    IInvoiceRepository invoiceRepository,
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork) : IInvoiceCommandService
{
    public async Task<Invoice?> Handle(CreateInvoiceCommand command)
    {
        var payment = await paymentRepository.FindByIdAsync(command.PaymentId);
        if (payment is null) return null;

        // Evita duplicado por PaymentId (1–1)
        var existing = (await invoiceRepository.FindByPaymentIdAsync(command.PaymentId)).FirstOrDefault();
        if (existing is not null) return null;

        var invoice = new Invoice(command.PaymentId, command.BookingId, command.UserId, command.Total);

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
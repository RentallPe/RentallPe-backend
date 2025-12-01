using RentalPeAPI.Payments.Application.ACL;
using RentalPeAPI.Payments.Application.Internal.CommandServices;
using RentalPeAPI.Payments.Application.Internal.QueryServices;
using RentalPeAPI.Payments.Domain.Repositories;
using RentalPeAPI.Payments.Domain.Services.invoice;
using RentalPeAPI.Payments.Domain.Services.payment;
using RentalPeAPI.Payments.Infrastructure.Persistence.EFC.Repositories;
using RentalPeAPI.Payments.Interfaces.ACL;

namespace RentalPeAPI.Payments.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPaymentsContextServices(this WebApplicationBuilder builder)
    {
        // Repos
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

        // Services (Application layer)
        builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
        builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
        builder.Services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
        builder.Services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();
        
        builder.Services.AddScoped<IPaymentsContextFacade, PaymentsContextFacade>();
    }
}
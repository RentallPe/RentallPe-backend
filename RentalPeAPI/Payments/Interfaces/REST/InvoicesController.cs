using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Payments.Domain.Model.Commands.Invoices;
using RentalPeAPI.Payments.Domain.Model.Enums;
using RentalPeAPI.Payments.Domain.Model.Queries.Invoices;
using RentalPeAPI.Payments.Domain.Services.invoice;
using RentalPeAPI.Payments.Interfaces.REST.Resources.invoices;
using RentalPeAPI.Payments.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.Payments.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Invoices")]
public class InvoicesController(
    IInvoiceCommandService invoiceCommandService,
    IInvoiceQueryService invoiceQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Gets an invoice by id", OperationId = "GetInvoiceById")]
    [SwaggerResponse(200, "Invoice found", typeof(InvoiceResource))]
    [SwaggerResponse(404, "Invoice not found")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var result = await invoiceQueryService.Handle(new GetInvoiceByIdQuery(id));
        if (result is null) return NotFound();
        return Ok(InvoiceResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Search invoices",
        Description = "Query by userId, paymentId or status",
        OperationId = "GetInvoicesFromQuery")]
    [SwaggerResponse(200, "Invoices found", typeof(IEnumerable<InvoiceResource>))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> GetInvoicesFromQuery(
        [FromQuery] int? userId,
        [FromQuery] int? paymentId,
        [FromQuery] InvoiceStatus? status)
    {
        if (paymentId.HasValue)
        {
            var list = await invoiceQueryService.Handle(new GetInvoicesByPaymentIdQuery(paymentId.Value));
            return Ok(list.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (userId.HasValue)
        {
            var list = await invoiceQueryService.Handle(new GetInvoicesByUserIdQuery(userId.Value));
            return Ok(list.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (status.HasValue)
        {
            var list = await invoiceQueryService.Handle(new GetInvoicesByStatusQuery(status.Value));
            return Ok(list.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity));
        }

        return BadRequest();
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates an invoice", OperationId = "CreateInvoice")]
    [SwaggerResponse(201, "Invoice created", typeof(InvoiceResource))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await invoiceCommandService.Handle(command);

        if (result is null) return BadRequest();

        return CreatedAtAction(
            nameof(GetInvoiceById),
            new { id = result.Id },
            InvoiceResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPost("{id:int}/issue")]
    [SwaggerOperation(Summary = "Issues an invoice", OperationId = "IssueInvoice")]
    [SwaggerResponse(200, "Invoice issued", typeof(InvoiceResource))]
    [SwaggerResponse(400, "Invalid state transition")]
    [SwaggerResponse(404, "Invoice not found")]
    public async Task<IActionResult> IssueInvoice(int id)
    {
        var result = await invoiceCommandService.Handle(new IssueInvoiceCommand(id));
        if (result is null) return NotFound();
        return Ok(InvoiceResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPost("{id:int}/send-email")]
    [SwaggerOperation(Summary = "Sends the invoice by email", OperationId = "SendInvoiceEmail")]
    [SwaggerResponse(200, "Invoice email processed", typeof(InvoiceResource))]
    [SwaggerResponse(400, "Invalid state transition")]
    [SwaggerResponse(404, "Invoice not found")]
    public async Task<IActionResult> SendInvoiceEmail(int id)
    {
        var result = await invoiceCommandService.Handle(new SendInvoiceEmailCommand(id));
        if (result is null) return NotFound();
        return Ok(InvoiceResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPost("{id:int}/void")]
    [SwaggerOperation(Summary = "Voids an invoice", OperationId = "VoidInvoice")]
    [SwaggerResponse(200, "Invoice voided", typeof(InvoiceResource))]
    [SwaggerResponse(400, "Invalid state transition")]
    [SwaggerResponse(404, "Invoice not found")]
    public async Task<IActionResult> VoidInvoice(int id)
    {
        var result = await invoiceCommandService.Handle(new VoidInvoiceCommand(id));
        if (result is null) return NotFound();
        return Ok(InvoiceResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
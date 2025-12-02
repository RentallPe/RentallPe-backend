using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.subscriptions.Domain.Model.Commands;
using RentalPeAPI.subscriptions.Domain.Model.Enums;
using RentalPeAPI.subscriptions.Domain.Model.Queries;
using RentalPeAPI.subscriptions.Domain.Services;
using RentalPeAPI.subscriptions.Interfaces.REST.Resources;
using RentalPeAPI.subscriptions.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.subscriptions.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Subscriptions")]
public class SubscriptionsController(
    ISubscriptionCommandService commandService,
    ISubscriptionQueryService queryService) : ControllerBase
{
    // GET /api/v1/subscriptions/{id}
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get subscription by id", OperationId = "GetSubscriptionById")]
    [SwaggerResponse(200, "Subscription found", typeof(SubscriptionResource))]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var result = await queryService.Handle(new GetSubscriptionByIdQuery(id));
        if (result is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    // GET /api/v1/subscriptions?customerId=&status=
    [HttpGet]
    [SwaggerOperation(
        Summary = "Search subscriptions",
        Description = "Filter by customerId and/or status",
        OperationId = "GetSubscriptionsFromQuery")]
    [SwaggerResponse(200, "Subscriptions found", typeof(IEnumerable<SubscriptionResource>))]
    public async Task<IActionResult> GetSubscriptionsFromQuery(
        [FromQuery] int? customerId,
        [FromQuery] SubscriptionStatus? status)
    {
        if (customerId.HasValue || status.HasValue)
        {
            if (status.HasValue)
            {
                var list = await queryService.Handle(
                    new GetSubscriptionsByStatusQuery(status.Value, customerId));
                return Ok(list.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
            }

            if (customerId.HasValue)
            {
                var list = await queryService.Handle(
                    new GetSubscriptionsByCustomerIdQuery(customerId.Value));
                return Ok(list.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
            }
        }

        // si no hay filtros, podrías decidir devolver ACTIVE por defecto o 400.
        var active = await queryService.Handle(
            new GetSubscriptionsByStatusQuery(SubscriptionStatus.ACTIVE, null));
        return Ok(active.Select(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // POST /api/v1/subscriptions
    [HttpPost]
    [SwaggerOperation(Summary = "Create subscription", OperationId = "CreateSubscription")]
    [SwaggerResponse(201, "Subscription created", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> CreateSubscription([FromBody] CreateSubscriptionResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = CreateSubscriptionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result  = await commandService.Handle(command);

        if (result is null) return BadRequest();

        var subscriptionResource = SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result);
        return CreatedAtAction(nameof(GetSubscriptionById), new { id = result.Id }, subscriptionResource);
    }

    // POST /api/v1/subscriptions/{id}/cancel
    [HttpPost("{id:int}/cancel")]
    [SwaggerOperation(Summary = "Cancel subscription", OperationId = "CancelSubscription")]
    [SwaggerResponse(200, "Subscription canceled", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "Invalid state transition")]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> CancelSubscription(int id)
    {
        var result = await commandService.Handle(new CancelSubscriptionCommand(id));
        if (result is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    // POST /api/v1/subscriptions/{id}/expire
    [HttpPost("{id:int}/expire")]
    [SwaggerOperation(Summary = "Expire subscription", OperationId = "ExpireSubscription")]
    [SwaggerResponse(200, "Subscription expired", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "Invalid state transition")]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> ExpireSubscription(int id)
    {
        var result = await commandService.Handle(new ExpireSubscriptionCommand(id));
        if (result is null) return NotFound();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    // POST /api/v1/subscriptions/{id}/change-plan
    [HttpPost("{id:int}/change-plan")]
    [SwaggerOperation(Summary = "Change subscription plan", OperationId = "ChangeSubscriptionPlan")]
    [SwaggerResponse(200, "Plan changed", typeof(SubscriptionResource))]
    [SwaggerResponse(400, "Invalid request")]
    [SwaggerResponse(404, "Subscription not found")]
    public async Task<IActionResult> ChangeSubscriptionPlan(
        int id,
        [FromBody] ChangePlanResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = new ChangeSubscriptionPlanCommand(
            SubscriptionId: id,
            NewPlan:        resource.NewPlan,
            NewPrice:       resource.NewPrice);

        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();
        return Ok(SubscriptionResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
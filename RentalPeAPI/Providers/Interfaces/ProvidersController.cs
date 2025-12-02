using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.providers.Domain.Model.Commands;
using RentalPeAPI.providers.Domain.Model.Queries;
using RentalPeAPI.providers.Domain.Repositories;
using RentalPeAPI.providers.Domain.Services;
using RentalPeAPI.providers.Interfaces.REST.Resources;
using RentalPeAPI.providers.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.providers.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Providers")]
public class ProvidersController(
    IProviderCommandService commandService,
    IProviderQueryService queryService) : ControllerBase
{
    // GET /api/v1/providers/{id}
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get provider by id", OperationId = "GetProviderById")]
    [SwaggerResponse(200, "Provider found", typeof(ProviderResource))]
    [SwaggerResponse(404, "Provider not found")]
    public async Task<IActionResult> GetProviderById(int id)
    {
        var result = await queryService.Handle(new GetProviderByIdQuery(id));
        if (result is null) return NotFound();

        return Ok(ProviderResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    // GET /api/v1/providers
    [HttpGet]
    [SwaggerOperation(Summary = "Get all providers", OperationId = "GetAllProviders")]
    [SwaggerResponse(200, "Providers list", typeof(IEnumerable<ProviderResource>))]
    public async Task<IActionResult> GetAll()
    {
        var list = await queryService.Handle(new GetAllProvidersQuery());
        return Ok(list.Select(ProviderResourceFromEntityAssembler.ToResourceFromEntity));
    }

    // POST /api/v1/providers
    [HttpPost]
    [SwaggerOperation(Summary = "Create provider", OperationId = "CreateProvider")]
    [SwaggerResponse(201, "Provider created", typeof(ProviderResource))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> CreateProvider(
        [FromBody] CreateProviderResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = CreateProviderCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        var result = await commandService.Handle(command);
        if (result is null) return BadRequest();

        var providerResource =
            ProviderResourceFromEntityAssembler.ToResourceFromEntity(result);

        return CreatedAtAction(
            nameof(GetProviderById),
            new { id = result.Id },
            providerResource);
    }
}

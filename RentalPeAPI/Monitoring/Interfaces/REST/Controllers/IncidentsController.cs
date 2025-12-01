// Monitoring/Interfaces/REST/Controllers/IncidentsController.cs
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IIncidentRepository _incidentRepository; 

    public IncidentsController(IMediator mediator, IIncidentRepository incidentRepository)
    {
        _mediator = mediator;
        _incidentRepository = incidentRepository;
    }

    /// <summary>
    /// GET: Obtiene la lista de incidentes para un proyecto,
    /// con el mismo shape que el "incidents" del db.json.
    /// </summary>
    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> ListIncidents(int projectId)
    {
        var incidents = await _incidentRepository.ListByProjectAsync(projectId);

        // Mapear entidad de dominio → resource/DTO para el front
        var resources = incidents.Select(i => new IncidentResource(
            i.Id,
            i.ProjectId,
            i.Description,
            i.Status,
            i.CreatedAt,
            i.UpdatedAt
        ));

        return Ok(resources);
    }

    /// <summary>
    /// PATCH: Marca un incidente como reconocido por un operador.
    /// </summary>
    [HttpPatch("{id:int}/acknowledge")]
    public async Task<IActionResult> AcknowledgeIncident(int id, [FromBody] AcknowledgeIncidentResource resource)
    {
        var command = new AcknowledgeIncidentCommand(id, resource.AcknowledgedByUserId);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent(); // 204 No Content
    }
}
// Monitoring/Interfaces/REST/Controllers/IncidentsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Domain.Repositories;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;
using System.Threading.Tasks;

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
    /// GET: Obtiene la lista de incidentes activos para un proyecto.
    /// </summary>
    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> ListIncidents(int projectId)
    {
        // Se asume que ListByProjectAsync ya trae el DTO o la entidad para mapear
        var incidents = await _incidentRepository.ListByProjectAsync(projectId);
        return Ok(incidents); 
    }

    /// <summary>
    /// PATCH: Marca un incidente como reconocido por un operador.
    /// </summary>
    [HttpPatch("{id:int}/acknowledge")]
    public async Task<IActionResult> AcknowledgeIncident(int id, [FromBody] AcknowledgeIncidentResource resource)
    {
        // Se asume que AcknowledgeIncidentResource solo tiene AcknowledgedByUserId
        var command = new AcknowledgeIncidentCommand(id, resource.AcknowledgedByUserId);
        var success = await _mediator.Send(command);

        if (!success) return NotFound();
        return NoContent(); // 204 No Content
    }
    
    
}
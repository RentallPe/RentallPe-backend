// Monitoring/Interfaces/REST/Controllers/ProjectsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] 
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea un nuevo proyecto y lo enlaza a una propiedad/usuario.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectResource resource)
    {
        
        var command = new CreateProjectCommand(
            resource.PropertyId,
            resource.UserId,
            resource.Name,
            resource.Description,
            resource.StartDate,
            resource.EndDate
        );
        
        
        var projectId = await _mediator.Send(command);
        
        
        return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, new { id = projectId }); 
    }

    /// <summary>
    /// GET: Obtiene los detalles de un proyecto.
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetProjectById(int id)
    {
        
        return Ok($"Proyecto {id} creado y listo para usar.");
    }
}
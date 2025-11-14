// Monitoring/Interfaces/REST/Controllers/ProjectsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] // Ruta: /api/v1/monitoring/projects
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
        // 1. Convertir Resource a Command (Mapeo explícito o Ensamblador)
        var command = new CreateProjectCommand(
            resource.PropertyId,
            resource.UserId,
            resource.Name,
            resource.Description,
            resource.StartDate,
            resource.EndDate
        );
        
        // 2. Enviar y obtener el ID
        var projectId = await _mediator.Send(command);
        
        // 3. Devolver 201 Created
        return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, new { id = projectId }); 
    }

    /// <summary>
    /// GET: Obtiene los detalles de un proyecto.
    /// </summary>
    [HttpGet("{id:int}")]
    public IActionResult GetProjectById(int id)
    {
        // Placeholder, pero ahora funcional
        return Ok($"Proyecto {id} creado y listo para usar.");
    }
}
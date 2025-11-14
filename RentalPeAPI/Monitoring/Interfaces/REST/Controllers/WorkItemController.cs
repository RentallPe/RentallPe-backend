// Monitoring/Interfaces/REST/Controllers/TasksController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] 
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Crea una nueva orden de trabajo, usualmente en respuesta a un incidente.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateWorkItemResource resource)
    {
        
        var command = new CreateWorkItemCommand(
            resource.ProjectId,
            resource.IncidentId,
            resource.AssignedToUserId,
            resource.Description
        );
        
        var taskId = await _mediator.Send(command);
        
       
        return CreatedAtAction(nameof(GetTaskById), new { id = taskId }, new { TaskId = taskId });
    }
    
    
    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        return Ok($"Tarea {id} creada y lista para ser consultada.");
    }
}
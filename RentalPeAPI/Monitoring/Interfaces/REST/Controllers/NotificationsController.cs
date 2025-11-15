// Monitoring/Interfaces/REST/Controllers/NotificationsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.QueryServices;
using RentalPeAPI.Monitoring.Domain.Entities; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] // Ruta: /api/v1/monitoring/notifications
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// GET: Obtiene el historial de notificaciones enviadas para un proyecto.
    /// </summary>
    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> ListNotificationsByProject(int projectId)
    {
        var query = new ListNotificationsQuery(projectId);
        var notifications = await _mediator.Send(query);
        
        return Ok(notifications); 
    }
}
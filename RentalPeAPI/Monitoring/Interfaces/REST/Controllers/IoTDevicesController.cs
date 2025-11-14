// Monitoring/Interfaces/REST/Controllers/IoTDevicesController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.QueryServices;
using RentalPeAPI.Monitoring.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices; // Nuevo using
using RentalPeAPI.Monitoring.Interfaces.REST.Resources;
namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] // Ruta: /api/v1/monitoring/iotdevices
public class IoTDevicesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IoTDevicesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// POST: Registra un nuevo dispositivo IoT.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] CreateIoTDeviceResource resource)
    {
        // 1. Mapear Resource a Command
        var command = new CreateIoTDeviceCommand(
            resource.ProjectId, 
            resource.Name, 
            resource.SerialNumber, 
            resource.Type
        );

        // 2. Enviar el comando al Command Handler
        var newDevice = await _mediator.Send(command);

        // 3. Retornar 201 Created, apuntando al recurso creado
        return CreatedAtAction(
            nameof(ListDevicesByProject), 
            new { projectId = newDevice.ProjectId }, // Parámetros para construir la URL (ej. GET)
            newDevice // Cuerpo de la respuesta
        );
    }
    /// <summary>
    /// GET: Lista todos los dispositivos instalados para un proyecto.
    /// </summary>
    [HttpGet("project/{projectId:int}")]
    public async Task<IActionResult> ListDevicesByProject(int projectId)
    {
        var query = new ListIoTDevicesByProjectQuery(projectId);
        var devices = await _mediator.Send(query);
        
        // Asumo que el mapeo a DTO (resource) se hará en una capa posterior o el frontend lo manejará.
        return Ok(devices); 
    }
}

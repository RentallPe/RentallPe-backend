// Monitoring/Interfaces/REST/Controllers/ReadingsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources; // Asumiendo que tu DTO está aquí

namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] // Ruta base: /api/v1/monitoring/readings
public class ReadingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReadingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Endpoint para la ingesta de telemetría de dispositivos IoT.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> IngestReading([FromBody] IngestReadingResource resource)
    {
        // Nota: Asume que este DTO (IngestReadingResource) ya existe
        // y tiene las propiedades necesarias (IoTDeviceId, Value, etc.)

        var command = new IngestReadingCommand(
            resource.IoTDeviceId,
            resource.ProjectId,
            resource.MetricName,
            resource.Value,
            resource.Unit,
            resource.Timestamp
        );
        
        await _mediator.Send(command);
        
        // Devolver 202 Accepted (La recepción es exitosa, el procesamiento es asíncrono)
        return Accepted();
    }
}
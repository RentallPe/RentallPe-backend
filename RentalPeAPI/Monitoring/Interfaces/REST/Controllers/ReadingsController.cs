// Monitoring/Interfaces/REST/Controllers/ReadingsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Monitoring.Application.Internal.CommandServices;
using RentalPeAPI.Monitoring.Interfaces.REST.Resources; 
using RentalPeAPI.Monitoring.Application.Internal.QueryServices;
namespace RentalPeAPI.Monitoring.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/monitoring/[controller]")] 
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
        

        var command = new IngestReadingCommand(
            resource.IoTDeviceId,
            resource.ProjectId,
            resource.MetricName,
            resource.Value,
            resource.Unit,
            resource.Timestamp
        );
        
        await _mediator.Send(command);
        
        
        return Accepted();
    }
}
using MediatR; 
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.User.Application.Internal.CommandServices;
using RentalPeAPI.User.Application.Internal.QueryServices;
using RentalPeAPI.User.Interfaces.REST.Resources;

namespace RentalPeAPI.User.Interfaces.REST.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserResource resource)
    {
        
        var command = new RegisterUserCommand(
            resource.FullName,
            resource.Email,
            resource.Password,
            resource.Phone,       // NUE 2025-11-15 Braulio
            resource.Role,        // NUE 2025-11-15 Braulio
            resource.ProviderId,  // NUE 2025-11-15 Braulio
            resource.Photo        // NUE 2025-11-15 Braulio

        );

       
        var userDto = await _mediator.Send(command);

     
        return CreatedAtAction(nameof(GetUserById), new { userId = userDto.Id }, userDto);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginResource resource)
    {
        try
        {
            var query = new LoginQuery(resource.Email, resource.Password);
            var authDto = await _mediator.Send(query);
            
            return Ok(authDto); 
        }
        catch (Exception ex)
        {
            
            return Unauthorized(new { message = ex.Message }); 
        }
    }
    
    [HttpGet("{userId:guid}")] 
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        
        var query = new GetUserByIdQuery(userId);

        
        var userDto = await _mediator.Send(query);

        
        return userDto is null ? NotFound() : Ok(userDto);
    }
}
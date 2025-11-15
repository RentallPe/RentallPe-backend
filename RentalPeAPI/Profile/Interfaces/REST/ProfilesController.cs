using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Interfaces.REST.Resources;
using RentalPeAPI.Profile.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.Profile.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Profiles")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get profile by id", OperationId = "GetProfileById")]
    [SwaggerResponse(200, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await profileQueryService.Handle(new GetProfileByIdQuery(id));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Query profiles", Description = "Query by userId or email", OperationId = "GetProfilesFromQuery")]
    [SwaggerResponse(200, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(400, "Invalid query")]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetFromQuery([FromQuery] long? userId, [FromQuery] string? email)
    {
        if (userId.HasValue)
        {
            var p = await profileQueryService.Handle(new GetProfileByUserIdQuery(userId.Value));
            if (p is null) return NotFound();
            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(p));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var p = await profileQueryService.Handle(new GetProfileByEmailQuery(email));
            if (p is null) return NotFound();
            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(p));
        }

        return BadRequest();
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create profile", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "Profile created", typeof(ProfileResource))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> Create([FromBody] CreateProfileResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var cmd = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await profileCommandService.Handle(cmd);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/name")]
    [SwaggerOperation(Summary = "Update full name", OperationId = "UpdateProfileName")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateProfileNameResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/bio")]
    [SwaggerOperation(Summary = "Update bio", OperationId = "UpdateProfileBio")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateBio(int id, [FromBody] UpdateProfileBioResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/email")]
    [SwaggerOperation(Summary = "Update email", OperationId = "UpdateProfileEmail")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateEmail(int id, [FromBody] UpdateProfileEmailResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/phone")]
    [SwaggerOperation(Summary = "Update phone", OperationId = "UpdateProfilePhone")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdatePhone(int id, [FromBody] UpdateProfilePhoneResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/address")]
    [SwaggerOperation(Summary = "Update address", OperationId = "UpdateProfileAddress")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateProfileAddressResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/avatar")]
    [SwaggerOperation(Summary = "Update avatar", OperationId = "UpdateProfileAvatar")]
    [SwaggerResponse(200, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateAvatar(int id, [FromBody] UpdateProfileAvatarResource resource)
    {
        var result = await profileCommandService.Handle(UpdateProfileCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
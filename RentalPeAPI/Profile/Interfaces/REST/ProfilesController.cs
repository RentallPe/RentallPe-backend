using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Interfaces.REST.Resources;
using RentalPeAPI.Profile.Interfaces.REST.Transform;

namespace RentalPeAPI.Profile.Interfaces.REST;

/// <summary>
///     Profiles controller – expone los endpoints del bounded context Profile.
/// </summary>
/// <param name="profileQueryService">Servicio de consultas de perfiles.</param>
/// <param name="profileCommandService">Servicio de comandos de perfiles.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile endpoints")]
public class ProfilesController(
    IProfileQueryService profileQueryService,
    IProfileCommandService profileCommandService) : ControllerBase
{
    /// <summary>
    ///     Obtiene un perfil por su Id.
    /// </summary>
    /// <param name="profileId">Identificador del perfil.</param>
    [HttpGet("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Get a profile by id",
        Description = "Get a profile by its identifier",
        OperationId = "GetProfileById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was found", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile was not found")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var query = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(query);
        if (profile is null) return NotFound();

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(resource);
    }

    /// <summary>
    ///     Obtiene un perfil por el Id de usuario (IAM/User BC).
    /// </summary>
    /// <param name="userId">Identificador del usuario IAM.</param>
    [HttpGet("by-user/{userId:long}")]
    [SwaggerOperation(
        Summary = "Get a profile by IAM user id",
        Description = "Get the profile associated to an IAM user identifier",
        OperationId = "GetProfileByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was found", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile was not found")]
    public async Task<IActionResult> GetProfileByUserId(long userId)
    {
        var query = new GetProfileByUserIdQuery(userId);
        var profile = await profileQueryService.Handle(query);
        if (profile is null) return NotFound();

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(resource);
    }

    /// <summary>
    ///     Crea un nuevo perfil.
    /// </summary>
    /// <param name="resource">Datos del perfil.</param>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new profile",
        Description = "Create a new profile for a given IAM user",
        OperationId = "CreateProfile")]
    [SwaggerResponse(StatusCodes.Status201Created, "The profile was created", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The profile could not be created")]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileResource resource)
    {
        var command = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return BadRequest();

        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }

    /// <summary>
    ///     Actualiza un perfil existente.
    /// </summary>
    /// <param name="profileId">Identificador del perfil.</param>
    /// <param name="resource">Datos del perfil a actualizar.</param>
    [HttpPut("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Update a profile",
        Description = "Update profile basic information",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(StatusCodes.Status200OK, "The profile was updated", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The profile was not found")]
    public async Task<IActionResult> UpdateProfile(
        int profileId,
        [FromBody] UpdateProfileResource resource)
    {
        var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(resource, profileId);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return NotFound();

        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
}

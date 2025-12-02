using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Profiles.Domain.Model.Queries;
using RentalPeAPI.Profiles.Domain.Services;
using RentalPeAPI.Profiles.Interfaces.REST.Resources;
using RentalPeAPI.Profiles.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Profile management endpoints.")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
    : ControllerBase
{
    /// <summary>
    /// Get profile by id.
    /// </summary>
    [HttpGet("{profileId:int}")]
    [SwaggerOperation("GetProfileById", "Get a profile by its unique identifier.")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var query = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(query);
        if (profile is null) return NotFound();

        var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(resource);
    }

    /// <summary>
    /// Get all profiles.
    /// </summary>
    [HttpGet]
    [SwaggerOperation("GetAllProfiles", "Get all profiles.")]
    [SwaggerResponse(200, "The profiles were found and returned.", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfiles()
    {
        var query = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(query);
        var resources = ProfileResourceFromEntityAssembler.ToResourceFromEntities(profiles);
        return Ok(resources);
    }

    /// <summary>
    /// Create a new profile.
    /// </summary>
    [HttpPost]
    [SwaggerOperation("CreateProfile", "Create a new profile.")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var command = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var profile = await profileCommandService.Handle(command);
        if (profile is null) return BadRequest();

        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileById),
            new { profileId = profile.Id },
            profileResource);
    }

    /// <summary>
    /// Add a payment method to a profile.
    /// </summary>
    [HttpPost("{profileId:int}/payment-methods")]
    [SwaggerOperation("AddPaymentMethod", "Add a payment method to a profile.")]
    [SwaggerResponse(201, "The payment method was added.", typeof(PaymentMethodResource))]
    [SwaggerResponse(400, "The payment method was not added.")]
    public async Task<IActionResult> AddPaymentMethod(int profileId, AddPaymentMethodResource resource)
    {
        var command = AddPaymentMethodCommandFromResourceAssembler.ToCommandFromResource(profileId, resource);
        var paymentMethod = await profileCommandService.Handle(command);
        if (paymentMethod is null) return BadRequest();

        var paymentMethodResource = new PaymentMethodResource(
            paymentMethod.Id,
            paymentMethod.Type,
            paymentMethod.Number,
            paymentMethod.Expiry,
            paymentMethod.Cvv);

        return CreatedAtAction(nameof(GetProfileById),
            new { profileId },
            paymentMethodResource);
    }
}
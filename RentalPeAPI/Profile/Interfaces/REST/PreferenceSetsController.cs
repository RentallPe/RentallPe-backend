using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Profile.Domain.Model.Commands;
using RentalPeAPI.Profile.Domain.Model.Enums;
using RentalPeAPI.Profile.Domain.Model.Queries;
using RentalPeAPI.Profile.Domain.Services;
using RentalPeAPI.Profile.Interfaces.REST.Resources;
using RentalPeAPI.Profile.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RentalPeAPI.Profile.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Preference Sets")]
public class PreferenceSetsController(
    IPreferenceSetCommandService preferenceCommandService,
    IPreferenceSetQueryService preferenceQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get preference set by id", OperationId = "GetPreferenceSetById")]
    [SwaggerResponse(200, "Preference set found", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await preferenceQueryService.Handle(new GetPreferenceSetByIdQuery(id));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Query preference sets", Description = "Query by userId, language or theme", OperationId = "GetPreferenceSetsFromQuery")]
    [SwaggerResponse(200, "Preference set(s) found", typeof(IEnumerable<PreferenceSetResource>))]
    [SwaggerResponse(400, "Invalid query")]
    public async Task<IActionResult> GetFromQuery([FromQuery] long? userId, [FromQuery] LanguageCode? language, [FromQuery] ThemeMode? theme)
    {
        if (userId.HasValue)
        {
            var one = await preferenceQueryService.Handle(new GetPreferenceSetByUserIdQuery(userId.Value));
            if (one is null) return NotFound();
            return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(one));
        }

        if (language.HasValue)
        {
            var list = await preferenceQueryService.Handle(new GetPreferenceSetsByLanguageQuery(language.Value));
            return Ok(list.Select(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity));
        }

        if (theme.HasValue)
        {
            var list = await preferenceQueryService.Handle(new GetPreferenceSetsByThemeQuery(theme.Value));
            return Ok(list.Select(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity));
        }

        return BadRequest();
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create preference set", OperationId = "CreatePreferenceSet")]
    [SwaggerResponse(201, "Preference set created", typeof(PreferenceSetResource))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> Create([FromBody] CreatePreferenceSetResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var cmd = CreatePreferenceSetCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await preferenceCommandService.Handle(cmd);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/language")]
    [SwaggerOperation(Summary = "Update language", OperationId = "UpdatePreferenceLanguage")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> UpdateLanguage(int id, [FromBody] UpdatePreferenceLanguageResource resource)
    {
        var result = await preferenceCommandService.Handle(new UpdatePreferenceLanguageCommand(id, resource.Language));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/theme")]
    [SwaggerOperation(Summary = "Update theme", OperationId = "UpdatePreferenceTheme")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> UpdateTheme(int id, [FromBody] UpdatePreferenceThemeResource resource)
    {
        var result = await preferenceCommandService.Handle(new UpdatePreferenceThemeCommand(id, resource.Theme));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/time-zone")]
    [SwaggerOperation(Summary = "Update time zone", OperationId = "UpdatePreferenceTimeZone")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> UpdateTimeZone(int id, [FromBody] UpdatePreferenceTimeZoneResource resource)
    {
        var result = await preferenceCommandService.Handle(new UpdatePreferenceTimeZoneCommand(id, resource.TimeZone));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/notifications")]
    [SwaggerOperation(Summary = "Set notifications", OperationId = "SetPreferenceNotifications")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> SetNotifications(int id, [FromBody] SetPreferenceNotificationsResource resource)
    {
        var result = await preferenceCommandService.Handle(
            UpdatePreferenceCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/privacy")]
    [SwaggerOperation(Summary = "Set privacy", OperationId = "SetPreferencePrivacy")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> SetPrivacy(int id, [FromBody] SetPreferencePrivacyResource resource)
    {
        var result = await preferenceCommandService.Handle(
            UpdatePreferenceCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/quiet-hours")]
    [SwaggerOperation(Summary = "Set quiet hours", OperationId = "SetQuietHours")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> SetQuietHours(int id, [FromBody] SetQuietHoursResource resource)
    {
        var result = await preferenceCommandService.Handle(
            UpdatePreferenceCommandsFromResourceAssembler.From(id, resource));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id:int}/quiet-hours")]
    [SwaggerOperation(Summary = "Clear quiet hours", OperationId = "ClearQuietHours")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> ClearQuietHours(int id)
    {
        var result = await preferenceCommandService.Handle(new ClearQuietHoursCommand(id));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPost("{id:int}/favorites")]
    [SwaggerOperation(Summary = "Add favorite remodeling", OperationId = "AddFavorite")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> AddFavorite(int id, [FromBody] AddFavoriteResource resource)
    {
        var result = await preferenceCommandService.Handle(new AddFavoriteCommand(id, resource.RemodelingId));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id:int}/favorites")]
    [SwaggerOperation(Summary = "Remove favorite remodeling", OperationId = "RemoveFavorite")]
    [SwaggerResponse(200, "Preference set updated", typeof(PreferenceSetResource))]
    [SwaggerResponse(404, "Preference set not found")]
    public async Task<IActionResult> RemoveFavorite(int id, [FromBody] RemoveFavoriteResource resource)
    {
        var result = await preferenceCommandService.Handle(new RemoveFavoriteCommand(id, resource.RemodelingId));
        if (result is null) return NotFound();
        return Ok(PreferenceSetResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}
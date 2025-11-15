using Microsoft.AspNetCore.Mvc;
using RentalPeAPI.Combo.Application.Internal.CommandServices;
using RentalPeAPI.Combo.Application.Internal.QueryServices;
using RentalPeAPI.Combo.Interfaces.REST.Transform;
using RentalPeAPI.Combo.Interfaces.REST.Resources;


namespace RentalPeAPI.Combo.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ComboController : ControllerBase
    {
        private readonly ComboCommandService _commandService;
        private readonly ComboQueryService _queryService;

        public ComboController(
            ComboCommandService commandService,
            ComboQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        // GET: api/v1/combo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboResource>>> GetAll([FromQuery] Guid? providerId)
        {
            var dtos = await _queryService.ListAsync(providerId);
            var resources = dtos.Select(ComboResourceAssembler.ToResource);
            return Ok(resources);
        }

        // GET: api/v1/combo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboResource>> GetById(int id)
        {
            var dto = await _queryService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(ComboResourceAssembler.ToResource(dto));
        }

        // POST: api/v1/combo
        [HttpPost]
        public async Task<ActionResult<ComboResource>> Create([FromBody] CreateComboResource resource)
        {
            var command = ComboCommandAssembler.ToCommand(resource);
            var dto = await _commandService.HandleAsync(command);

            var result = ComboResourceAssembler.ToResource(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/v1/combo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateComboResource resource)
        {
            var command = ComboCommandAssembler.ToCommand(id, resource);
            var dto = await _commandService.HandleUpdateAsync(command);

            if (dto == null) return NotFound();
            return NoContent();
        }

        // DELETE: api/v1/combo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _commandService.HandleDeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
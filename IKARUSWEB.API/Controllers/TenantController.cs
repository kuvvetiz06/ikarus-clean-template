using Microsoft.AspNetCore.Mvc;
using MediatR;
using IKARUSWEB.Application.Commands.CreateTenant;
using IKARUSWEB.Application.DTOs;
using IKARUSWEB.Application.Queries.GetTenantById;
using IKARUSWEB.Application.Queries.GetAllTenants;
using IKARUSWEB.Application.Commands.UpdateTenant;
using IKARUSWEB.Application.Commands.DeleteTenant;

namespace IKARUSWEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantController(IMediator mediator)
            => _mediator = mediator;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDto>>> GetAll()
        {
            var list = await _mediator.Send(new GetAllTenantsQuery());
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTenantCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TenantDto>> GetById(Guid id)
        {
            // Burada bir GetTenantByIdQuery tanımlıysa onu kullanın
            // yoksa repository'yi doğrudan enjekte edelim:
            var tenant = await _mediator.Send(new GetTenantByIdQuery(id));
            if (tenant is null) return NotFound();
            return Ok(tenant);
        }

        // PUT api/tenant/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTenantCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _mediator.Send(cmd);
            return NoContent();
        }

        // DELETE api/tenant/{id}?modifiedUser=someUser
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] string modifiedUser)
        {
            if (string.IsNullOrWhiteSpace(modifiedUser))
                return BadRequest("modifiedUser boş olamaz.");

            var cmd = new DeleteTenantCommand(id, modifiedUser);
            await _mediator.Send(cmd);
            return NoContent();
        }
    }
}

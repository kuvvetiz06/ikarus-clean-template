using Microsoft.AspNetCore.Mvc;
using MediatR;
using IKARUSWEB.Application.Commands.CreateTenant;
using IKARUSWEB.Application.DTOs;
using IKARUSWEB.Application.Queries.GetTenantById;

namespace IKARUSWEB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TenantController(IMediator mediator)
            => _mediator = mediator;

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
    }
}

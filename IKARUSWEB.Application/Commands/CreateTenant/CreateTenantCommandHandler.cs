using IKARUSWEB.Application.Interfaces;
using IKARUSWEB.Domain.Entities;
using MediatR;


namespace IKARUSWEB.Application.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
    {
        private readonly ITenantRepository _repo;

        public CreateTenantCommandHandler(ITenantRepository repo) =>
            _repo = repo;

        public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken ct)
        {
            var tenant = new Tenant(
                request.Code,
                request.Name,
                request.Address,
                request.PhoneNumber,
                request.Email,
                request.CreatedUser
            );
            await _repo.AddAsync(tenant);
            return tenant.Id;
        }
    }
}

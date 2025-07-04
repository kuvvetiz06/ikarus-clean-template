using IKARUSWEB.Application.Interfaces;
using MediatR;


namespace IKARUSWEB.Application.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, Unit>
    {
        private readonly ITenantRepository _repo;

        public UpdateTenantCommandHandler(ITenantRepository repo)
            => _repo = repo;

        public async Task<Unit> Handle(UpdateTenantCommand request, CancellationToken ct)
        {
            var tenant = await _repo.GetByIdAsync(request.Id);
            if (tenant is null)
                throw new KeyNotFoundException($"Tenant (Id: {request.Id}) bulunamadı.");

            tenant.SetCode(request.Code);
            tenant.SetName(request.Name);
            tenant.SetAddress(request.Address);
            tenant.SetPhoneNumber(request.PhoneNumber);
            tenant.SetEmail(request.Email);
            tenant.MarkModified(request.ModifiedUser);

            await _repo.UpdateAsync(tenant);
            return Unit.Value;
        }
    }
}

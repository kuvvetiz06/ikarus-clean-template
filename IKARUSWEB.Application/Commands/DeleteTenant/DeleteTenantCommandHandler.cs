using IKARUSWEB.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, Unit>
    {
        private readonly ITenantRepository _repo;

        public DeleteTenantCommandHandler(ITenantRepository repo) =>
            _repo = repo;

        public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken ct)
        {
            var tenant = await _repo.GetByIdAsync(request.Id);
            if (tenant is null)
                throw new KeyNotFoundException($"Tenant (Id: {request.Id}) bulunamadı.");

            // Soft delete
            tenant.MarkDeleted();
            // Silme işlemini yapan kullanıcıyı da kaydet
            tenant.MarkModified(request.ModifiedUser);

            await _repo.UpdateAsync(tenant);
            return Unit.Value;
        }
    }
}

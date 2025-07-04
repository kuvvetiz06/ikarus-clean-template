using IKARUSWEB.Domain.Entities;

namespace IKARUSWEB.Application.Interfaces
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(Guid id);
        Task AddAsync(Tenant tenant);
        Task UpdateAsync(Tenant tenant);
    }
}

using IKARUSWEB.Application.Interfaces;
using IKARUSWEB.Domain.Entities;
using IKARUSWEB.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace IKARUSWEB.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IKARUSDbContext _context;
        public TenantRepository(IKARUSDbContext context) => _context = context;

        public async Task AddAsync(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task<Tenant?> GetByIdAsync(Guid id)
            => await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);
    }
}

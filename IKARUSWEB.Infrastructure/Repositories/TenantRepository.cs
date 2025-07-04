using IKARUSWEB.Application.Interfaces;
using IKARUSWEB.Domain.Entities;
using IKARUSWEB.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace IKARUSWEB.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IIKARUSDbContext _context;

        public TenantRepository(IIKARUSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
            // IIKARUSDbContext.SaveChangesAsync requires a CancellationToken parameter
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<Tenant?> GetByIdAsync(Guid id)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Tenant tenant)
        {
            // _context.Tenants.Update(tenant); // EF Core zaten track ediyor, ama explicit isterseniz
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}

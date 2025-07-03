using IKARUSWEB.Application.Interfaces;
using IKARUSWEB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IKARUSWEB.Infrastructure.Persistence
{
    public class IKARUSDbContext : DbContext, IIKARUSDbContext
    {
        public IKARUSDbContext(DbContextOptions<IKARUSDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; } = null!;


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => base.SaveChangesAsync(cancellationToken);
    }
}

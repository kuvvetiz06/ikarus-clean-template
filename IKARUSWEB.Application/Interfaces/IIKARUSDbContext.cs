using IKARUSWEB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Interfaces
{
    public interface IIKARUSDbContext
    {
        DbSet<Tenant> Tenants { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

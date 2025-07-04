using MediatR;
using IKARUSWEB.Application.DTOs;
using IKARUSWEB.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Queries.GetAllTenants
{
    public class GetAllTenantsQueryHandler
        : IRequestHandler<GetAllTenantsQuery, IEnumerable<TenantDto>>
    {
        private readonly IIKARUSDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTenantsQueryHandler(
            IIKARUSDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TenantDto>> Handle(
            GetAllTenantsQuery request,
            CancellationToken cancellationToken)
        {
            var tenants = await _context.Tenants
                .Where(t => !t.IsDeleted)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TenantDto>>(tenants);
        }
    }
}

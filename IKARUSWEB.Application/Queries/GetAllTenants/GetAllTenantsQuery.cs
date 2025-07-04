using IKARUSWEB.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Queries.GetAllTenants
{
    public record GetAllTenantsQuery() : IRequest<IEnumerable<TenantDto>>;
}

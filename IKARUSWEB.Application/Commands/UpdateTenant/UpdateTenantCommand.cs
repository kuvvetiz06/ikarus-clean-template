using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Commands.UpdateTenant
{
    public record UpdateTenantCommand(
        Guid Id,
        string Code,
        string Name,
        string Address,
        string PhoneNumber,
        string Email,
        string ModifiedUser
    ) : IRequest<Unit>;
}

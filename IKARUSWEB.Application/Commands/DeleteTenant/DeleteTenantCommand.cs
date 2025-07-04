using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKARUSWEB.Application.Commands.DeleteTenant
{
    // Soft delete için sadece Id ve kullanıcı bilgisi yeterli
    public record DeleteTenantCommand(Guid Id, string ModifiedUser) : IRequest<Unit>;
}

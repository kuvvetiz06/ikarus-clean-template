using MediatR;

namespace IKARUSWEB.Application.Commands.CreateTenant
{
    public record CreateTenantCommand(
          string Code,
          string Name,
          string Address,
          string PhoneNumber,
          string Email,
          string CreatedUser
      ) : IRequest<Guid>;

}

using FluentValidation;


namespace IKARUSWEB.Application.Queries.GetTenantById
{
    public class GetTenantByIdQueryValidator : AbstractValidator<GetTenantByIdQuery>
    {
        public GetTenantByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

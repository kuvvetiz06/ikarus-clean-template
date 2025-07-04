using AutoMapper;
using IKARUSWEB.Application.DTOs;
using IKARUSWEB.Application.Interfaces;
using MediatR;


namespace IKARUSWEB.Application.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDto?>
    {
        private readonly ITenantRepository _repo;
        private readonly IMapper _mapper;

        public GetTenantByIdQueryHandler(ITenantRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TenantDto?> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _repo.GetByIdAsync(request.Id);
            return tenant is null ? null : _mapper.Map<TenantDto>(tenant);
        }
    }
}

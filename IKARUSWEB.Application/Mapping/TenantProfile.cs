using IKARUSWEB.Application.DTOs;
using IKARUSWEB.Domain.Entities;
using AutoMapper;

namespace IKARUSWEB.Application.Mapping
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<Tenant, TenantDto>();
        }
    }
}

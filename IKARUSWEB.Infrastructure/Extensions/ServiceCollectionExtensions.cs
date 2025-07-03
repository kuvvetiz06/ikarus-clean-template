using IKARUSWEB.Application.Interfaces;
using IKARUSWEB.Infrastructure.Persistence;
using IKARUSWEB.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IKARUSWEB.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DbContext ve IIKARUSDbContext arayüzü ilişkisini kur
            services.AddDbContext<IIKARUSDbContext, IKARUSDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Repository kayıtları
            services.AddScoped<ITenantRepository, TenantRepository>();

            return services;
        }
    }
}

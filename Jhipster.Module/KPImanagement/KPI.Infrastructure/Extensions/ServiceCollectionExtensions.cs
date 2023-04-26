using System;
using Jhipster.Infrastructure.Shared;
using KPI.Application.Configuration.Mappers;
using KPI.Core.Abtractions;
using KPI.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KPI.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKPIInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<BaseDbContext>(config)
                .AddScoped<IBaseDbContext>(provider => provider.GetService<BaseDbContext>() ?? throw new ArgumentNullException(nameof(BaseDbContext)));

            // Đăng kí automapper
            services.AddAutoMapper(typeof(APIMapperProfile));



            //Đăng kí service
            return services;
        }
    }

}


using System;
using KPI.Core.Extensions;
using KPI.Infrastructure.Extensions;

namespace KPI
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddKPIModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddKPICore()
                .AddKPIInfrastructure(configuration);

            // worker
 

            return services;
        }

        public static IApplicationBuilder AddLicensingAppModule(this IApplicationBuilder app)
        {
            return app;
        }
    }

}


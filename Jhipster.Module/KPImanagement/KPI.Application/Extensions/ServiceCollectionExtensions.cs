using System;
using Microsoft.Extensions.DependencyInjection;

namespace KPI.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseApplication(this IServiceCollection services)
        {
            return services;
        }
    }

}


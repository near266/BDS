using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KPI.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKPICore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }

}


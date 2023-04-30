using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Wallet.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}


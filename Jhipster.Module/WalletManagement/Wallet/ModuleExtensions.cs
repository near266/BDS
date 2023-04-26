using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Extensions;
using Wallet.Infrastructure.Extensions;

namespace Wallet
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddWalletModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);
            return services;
        }
    }
}


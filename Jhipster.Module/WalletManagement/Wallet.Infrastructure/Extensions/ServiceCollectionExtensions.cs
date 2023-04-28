using System;
using Jhipster.Infrastructure.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Infrastructure.Persistences;
using Wallet.Infrastructure.Persistences.Repositories;

namespace Wallet.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<WalletDbContext>(config)
                .AddScoped<IWalletDbContext>(provider => provider.GetService<WalletDbContext>());
            // Đăng kí mediatR
            //services.AddMediatR(typeof(BrandAddCommand).Assembly);

            //// Đăng kí repository
            //services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
            services.AddScoped(typeof(IWalletRepository),typeof(WalletRepository));
            services.AddScoped(typeof(IWalletPromotionalRepository),typeof(WalletPromotionalRepository));
          
            return services;
        }
    }
}


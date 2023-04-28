using System;
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
using Wallet.Application.Commands.WalletsC;
=======
using Wallet.Application.Commands.WalletsPromotionaC;
>>>>>>> 16547066a251022d0be67cad4f465202e8901a83
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
<<<<<<< HEAD
            services.AddMediatR(typeof(AddWalletsCommand).Assembly);
=======
            services.AddMediatR(typeof(AddWalletPromotionCommand).Assembly);
>>>>>>> 16547066a251022d0be67cad4f465202e8901a83

            //// Đăng kí repository
            services.AddScoped(typeof(IWalletRepository), typeof(WalletRepository));
            services.AddScoped(typeof(IWalletPromotionalRepository), typeof(WalletPromotionalRepository));

            return services;
        }
    }
}


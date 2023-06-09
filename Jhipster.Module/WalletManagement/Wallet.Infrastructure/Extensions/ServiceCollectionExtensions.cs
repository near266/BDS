using System;
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Wallet.Application.Commands.WalletsC;

using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.Configurations.Mapper;
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

            //Đăng kí automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Đăng kí mediatR

            services.AddMediatR(typeof(AddWalletsCommand).Assembly);

            services.AddMediatR(typeof(AddWalletPromotionCommand).Assembly);


            //// Đăng kí repository
            services.AddScoped(typeof(IWalletRepository), typeof(WalletRepository));
            services.AddScoped(typeof(IWalletPromotionalRepository), typeof(WalletPromotionalRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(ITransactionHistoryRepository), typeof(TransactionHistoryRepository));
            services.AddScoped(typeof(IDepositRequestRepository), typeof(DepositRequestRepository));

            return services;
        }
    }
}


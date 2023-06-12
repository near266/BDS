using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Configurations.Mapper;
using WorkerSvc.Application.Persistences;
using WorkerSvc.Infrastructure.Repositories;

namespace WorkerSvc.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //services
            //    .AddDatabaseContext<WalletDbContext>(config)
            //    .AddScoped<IWalletDbContext>(provider => provider.GetService<WalletDbContext>());

            //Đăng kí automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            // Đăng kí mediatR


            //// Đăng kí repository
            services.AddScoped(typeof(IWorkerRepositories), typeof(WorkerRepositories));

            return services;
        }
    }
}


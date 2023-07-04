using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorrkerSvc.Domain.Extensions;
using WorkerSvc.Infrastructure.Extensions;
namespace Worker
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddWorkerSale(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);
            // services.AddScoped<UpdateStatusWorker>();

            services.AddHostedService<UpdateStatusWorker>();
            services.AddHostedService<UpdateOrderFakeNewWorker>();
            services.AddHostedService<RepostSalePostWorker>();
            return services;
        }
    }
}

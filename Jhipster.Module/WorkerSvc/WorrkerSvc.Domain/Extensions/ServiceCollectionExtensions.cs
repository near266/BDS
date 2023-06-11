using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace WorrkerSvc.Domain.Extensions
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


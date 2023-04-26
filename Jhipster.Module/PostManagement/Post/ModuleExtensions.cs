using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Domain.Extensions;
using Post.Infrastructure.Extensions;

namespace Post
{
	public static class ModuleExtensions
    {
        public static IServiceCollection AddPostModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddBaseCore()
                .AddBaseInfrastructure(configuration);
            return services;
        }
    }
}


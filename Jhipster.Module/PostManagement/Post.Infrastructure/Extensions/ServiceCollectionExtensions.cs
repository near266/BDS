using System;
using Jhipster.Infrastructure.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Configurations.Mapper;
using Post.Application.Contracts;
using Post.Domain.Abstractions;
using Post.Infrastructure.Persistences;

namespace Post.Infrastructure.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDatabaseContext<PostDbContext>(config)
                .AddScoped<IPostDbContext>(provider => provider.GetService<PostDbContext>());
            // Đăng kí mediatR
            services.AddMediatR(typeof(AddBoughtPostCommand).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            //// Đăng kí repository
            //services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));

            services.AddScoped(typeof(IPostRepository), typeof(Persistences.Repositories.PostRepository));
            services.AddScoped(typeof(IFakeNewRepository), typeof(Persistences.Repositories.FakeNewRepository));
            services.AddScoped(typeof(INotificationRepository), typeof(Persistences.Repositories.NotificationRepository));
            services.AddScoped(typeof(ICommentRepository), typeof(Persistences.Repositories.CommentRepository));

            return services;
        }
    }
}


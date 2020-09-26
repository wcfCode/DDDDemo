using DDD.Api;
using DDD.API.Application.IntegrationEvents;
using DDD.Domain.OrderAggregate;
using DDD.Infrastructure;
using DDD.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDD.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Order).Assembly, typeof(Program).Assembly);
        }


        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<DomainContext>(optionsAction);
        }

        public static IServiceCollection AddInMemoryDomainContext(this IServiceCollection services)
        {
            return services.AddDomainContext(builder => builder.UseInMemoryDatabase("domanContextDatabase"));
        }

        public static IServiceCollection AddNpgsqlDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseNpgsql(connectionString);
            });
        }


        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }



        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                //options.UseEntityFramework<DomainContext>();
                options.UsePostgreSql(configuration.GetValue<string>("pgsql"));
                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                //options.UseDashboard();
            });

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.API.Configuration.Constants;
using ProjectManagement.BLL.Common;
using ProjectManagement.EntityFramework.MySql.Extensions;
using ProjectManagement.EntityFramework.PostgreSQL.Extensions;
using ProjectManagement.EntityFramework.Shared.Configuration;
using ProjectManagement.EntityFramework.Shared.Repositories;
using ProjectManagement.EntityFramework.Shared.Repositories.Interfaces;
using ProjectManagement.EntityFramework.SqlServer.Extensions;
using System;
using System.Linq;

namespace ProjectManagement.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Екстеншен, який добавляє сервіси які реалізували IScopedService,ISingletonService,IService
        /// Логіка в тому, щоб кожен раз не підключати сервісі, які повинні 100% бути включні
        /// Є що покращити
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalServices(
            this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);
            var singletonServiceInterfaceType = typeof(ISingletonService);
            var scopedServiceInterfaceType = typeof(IScopedService);

            var types = serviceInterfaceType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }


        /// <summary>
        /// Register DbContext for Application
        /// Configure the connection string in AppSettings.json
        /// </summary>
        /// <typeparam name="TApplicationDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void AddDbContexts<TApplicationDbContext>(this IServiceCollection services,
            IConfiguration configuration)
            where TApplicationDbContext : DbContext
        {
            var databaseProvider = configuration.GetSection(nameof(DatabaseProviderConfiguration))
                .Get<DatabaseProviderConfiguration>();

            var applicationConnectionString =
                configuration.GetConnectionString(ConfigurationConsts.ApplicationDbConnectionStringKey);

            switch (databaseProvider.ProviderType)
            {
                case DatabaseProviderType.SqlServer:
                    services.RegisterSqlServerDbContexts<TApplicationDbContext>(applicationConnectionString);
                    break;
                case DatabaseProviderType.PostgreSQL:
                    services.RegisterNpgSqlDbContexts<TApplicationDbContext>(applicationConnectionString);
                    break;
                case DatabaseProviderType.MySql:
                    services.RegisterMySqlDbContexts<TApplicationDbContext>(applicationConnectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseProvider.ProviderType),
                        $@"The value needs to be one of {string.Join(", ", Enum.GetNames(typeof(DatabaseProviderType)))}.");
            }
        }
    }
}
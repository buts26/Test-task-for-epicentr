﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProjectManagement.EntityFramework.SqlServer.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Register DbContext for Application
        /// Configure the connection string in AppSettings.json
        /// </summary>
        /// <typeparam name="TApplicationDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="applicationConnectionString"></param>
        public static void RegisterSqlServerDbContexts<TApplicationDbContext>(this IServiceCollection services, string applicationConnectionString)
            where TApplicationDbContext : DbContext
        {
            var migrationsAssembly = typeof(DatabaseExtensions).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<TApplicationDbContext>(options => options.UseSqlServer(applicationConnectionString, 
                sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}

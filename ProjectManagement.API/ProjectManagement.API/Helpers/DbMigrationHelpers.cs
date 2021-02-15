using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.API.Configuration;
using ProjectManagement.API.Configuration.Interfaces;
using ProjectManagement.BLL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.BLL.Models.Project.Request;
using ProjectManagement.BLL.Models.Project.Response;
using ProjectManagement.BLL.Models.User.Request;
using ProjectManagement.BLL.Models.User.Response;

namespace ProjectManagement.API.Helpers
{
    public class DbMigrationHelpers
    {
        /// <summary>
        /// Perform migration with test data filling
        /// </summary>
        /// <typeparam name="TApplicationDbContext"></typeparam>
        /// <param name="host"></param>
        /// <param name="seedConfiguration"></param>
        /// <param name="databaseMigrationsConfiguration"></param>
        /// <returns></returns>
        public static async Task ApplyDbMigrationsWithDataSeedAsync<TApplicationDbContext>(
            IHost host, SeedConfiguration seedConfiguration,
            DatabaseMigrationsConfiguration databaseMigrationsConfiguration)
            where TApplicationDbContext : DbContext
        {
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            if ((databaseMigrationsConfiguration != null && databaseMigrationsConfiguration.ApplyDatabaseMigrations))
            {
                await EnsureDatabasesMigratedAsync<TApplicationDbContext>(services);
            }

            if ((seedConfiguration != null && seedConfiguration.ApplySeed))
            {
                await EnsureSeedDataAsync<TApplicationDbContext>(services);
            }
        }

        public static async Task EnsureDatabasesMigratedAsync<TApplicationDbContext>(IServiceProvider services)
            where TApplicationDbContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<TApplicationDbContext>();
            await context.Database.MigrateAsync();
        }

        public static async Task EnsureSeedDataAsync<TApplicationDbContext>(IServiceProvider serviceProvider)
            where TApplicationDbContext : DbContext
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var userService = serviceProvider.GetRequiredService<IUserService>();
            var projectService = serviceProvider.GetRequiredService<IProjectService>();
            var rootConfiguration = scope.ServiceProvider.GetRequiredService<IRootConfiguration>();

            //var createdProjects = new List<ProjectResult>();

            foreach (var project in rootConfiguration.ProjectDataConfiguration.Projects)
            {
                if (!await projectService.IsExist(project.Name))
                {
                   
                    await projectService.AddAsync(project);
                    //if (result != null)
                    //    createdProjects.Add(result);
                }
            }

            foreach (var user in rootConfiguration.UserDataConfiguration.Users)
            {
                if (!await userService.IsExist(user.Email))
                {
                    //user.Projects.AddRange(createdProjects.Select(x => new ProjectRequest() { Id = x.Id }).ToList()); // топорний спосіб
                    await userService.AddAsync(user);
                }
            }

        }

    }
}
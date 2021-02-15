using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectManagement.API.Configuration;
using ProjectManagement.API.Configuration.Constants;
using ProjectManagement.API.Configuration.Interfaces;
using ProjectManagement.API.Infrastructure.Extensions;
using ProjectManagement.API.Infrastructure.Middlewares;
using ProjectManagement.EntityFramework.Shared.DbContexts;

namespace ProjectManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var rootConfiguration = CreateRootConfiguration();
            services.AddSingleton(rootConfiguration);
            services.AddDbContexts<ApplicationDbContext>(Configuration);
            services.AddRepositories();
            services.AddConventionalServices();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectManagement.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectManagement.API v1"));
            }
            app.UseMiddleware<SerilogMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected IRootConfiguration CreateRootConfiguration()
        {
            var rootConfiguration = new RootConfiguration();
            Configuration.GetSection(ConfigurationConsts.UserConfigurationKey)
                .Bind(rootConfiguration.UserDataConfiguration);
            Configuration.GetSection(ConfigurationConsts.ProjectConfigurationKey)
                .Bind(rootConfiguration.ProjectDataConfiguration);
            return rootConfiguration;
        }
    }
}

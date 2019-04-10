using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Monitor.Server.HealthChecks;
using Monitor.Server.Monitorings;
using Monitor.Server.Monitorings.Interfaces;
using Monitor.Server.Sockets;
using Monitor.Server.Sockets.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Monitor.Server
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
            services.AddSingleton<IMonitoring, Monitoring>();
            services.AddTransient<IMonitorsSocket, MonitorsSocket>();

            services
                .AddHealthChecks()
                .AddCheck<HealthCheckSocket>("socket");

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Monitoring API", Version = "v1" });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    c.IncludeXmlComments(xmlPath);

                    c.ExampleFilters();
                    c.DescribeAllEnumsAsStrings();
                });

            services
                .AddSwaggerExamplesFromAssemblyOf<Program>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMonitorsSocket monitorSocket)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region "Swagger"
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Monitoring API v1");
                c.RoutePrefix = "api/swagger";
            });
            #endregion "Swagger"

            #region "WebSocket"
            app.UseWebSockets(MonitorsSocketEx.BuildOptions());
            app.UseSocketRoutes(monitorSocket);
            #endregion "WebSocket"


            #region "HealthCheck"
            app.UseHealthChecks("/ping", HealthCheckEx.BuildOptions());
            #endregion "HealthCheck"


            app.UseMvc();
        }
    }
}

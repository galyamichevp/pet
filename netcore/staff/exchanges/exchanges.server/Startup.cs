using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using exchanges.server.Configurations.Entities;
using exchanges.server.Infrastructure.Providers;
using exchanges.server.Infrastructure.Providers.Interfaces;
using exchanges.server.Infrastructure.Repositories;
using exchanges.server.Infrastructure.Repositories.Interfaces;
using exchanges.server.Quartz;
using exchanges.server.Quartz.Jobs;
using exchanges.server.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace exchanges.server
{
    public class Startup
    {
        private IServiceCollection _services;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;

            services.AddLogging(configure => configure.AddConsole());

            services.AddOptions();

            services.Configure<List<ProviderConfig>>(Configuration.GetSection(Constants.Providers.ProvidersSection));
            services.Configure<MongoConfig>(Configuration.GetSection(Constants.Mongo.MongoSection));

            services.AddTransient<IRedisRepository, RedisRepository>();
            services.AddTransient<IMongoRepository, MongoRepository>();
            services.AddTransient<ILocalExchangeProvider, LocalExchangeProvider>();
            services.AddTransient<IOERExchangeProvider, OERExchangeProvider>();
            services.AddTransient<IExchangeProviderFactory, ExchangeProviderFactory>();

            services.AddTransient<LocalJob>();
            services.AddTransient<OERJob>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Rates API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration.GetSection(Constants.Redis.RedisSection)[Constants.Redis.Host];
                option.InstanceName = Configuration.GetSection(Constants.Redis.RedisSection)[Constants.Redis.Instance];
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            IOptions<List<ProviderConfig>> providerConfigs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rates API V1");
            });

            app.UseMvc();

            var quartz = new QuartzStartup(_services.BuildServiceProvider(), providerConfigs);
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopping.Register(quartz.Stop);
        }
    }
}

﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnLineStore.Core;
using OnLineStore.Core.BusinessLayer;
using OnLineStore.Core.BusinessLayer.Contracts;
using OnLineStore.Core.DataLayer;

namespace OnLineStore.WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(Configuration["AppSettings:ConnectionString"]));

            services.AddScoped<IUserInfo, UserInfo>();

            services.AddScoped<ILogger, Logger<Service>>();

            services.AddScoped<IHumanResourcesService, HumanResourcesService>();
            services.AddScoped<IProductionService, ProductionService>();
            services.AddScoped<ISalesService, SalesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}

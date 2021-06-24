using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMongoBookAPI.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CoreMongoBookAPI.Repository;
using CoreMongoBookAPI.DatabaseServices;
using NLog;
using System.IO;
using CoreMongoBookAPI.Logger;
using CoreMongoBookAPI.Extention;

namespace CoreMongoBookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();

            services.AddControllers();
            services.Configure<Mongosettings>(Configuration.GetSection(nameof(Mongosettings)));

            services.AddSingleton<IMongosettings>(sp =>
                sp.GetRequiredService<IOptions<Mongosettings>>().Value);

            services.AddSingleton<EmployeeService>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text;

namespace StoreMiddleware
{
    public static class Extentions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("CatalogDBSetting");
            services.Configure<MongoOptions>(configuration.GetSection("CatalogDBSetting"));
            services.AddSingleton(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });
            services.AddSingleton(sp =>
            {
                var options = sp.GetService<IOptions<MongoOptions>>();
                var client = sp.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            services.Configure<JwtOptions>(section);
            //services.AddSingleton<IJwtBuilder, JwtBuilder>();
            //services.AddAuthentication()
            //.AddJwtBearer(cfg =>
            //{
            //    cfg.RequireHttpsMetadata = false;
            //    cfg.SaveToken = true;
            //    cfg.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret))
            //    };
            //});
        }
    }
}

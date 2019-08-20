﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmployeeWebAPITwo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableCors();

            //to enable HTTPS for the entire application
            //config.Filters.Add(new RequireHttpsAttribute());

            //
            config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}

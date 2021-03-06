﻿using System.Net.Http.Headers;
using System.Web.Http;

namespace adir.photography
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Return a Json formatted result (quicker and less data)
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html")); 

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}

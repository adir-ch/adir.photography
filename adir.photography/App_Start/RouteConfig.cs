using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace adir.photography
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "gallery",
               url: "gallery/{*catchall}",
               defaults: new { controller = "Home", action = "Gallery" }
            );

            routes.MapRoute(
               name: "about",
               url: "about/{*catchall}",
               defaults: new { controller = "Home", action = "about" }
            );

            routes.MapRoute(
               name: "member",
               url: "member/{*catchall}",
               defaults: new { controller = "Home", action = "member" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
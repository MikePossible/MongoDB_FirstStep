using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RetrogameWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Instagram Route
            routes.MapRoute(
                name: "Instagram",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Instagram", action = "Index", id = UrlParameter.Optional }
            );

            //Player Route
            routes.MapRoute(
                name: "Player",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Player", action = "Index", id = UrlParameter.Optional }
            );

            //Game Route
            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
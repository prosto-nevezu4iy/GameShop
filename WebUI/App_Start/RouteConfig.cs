using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "Shop/{alias}",
                defaults: new { controller = "Category", action = "List" }
            );

            routes.MapRoute(
                name: "SubCategory",
                url: "Shop/{alias}/{subAlias}",
                defaults: new {controller = "Product", action = "List"}
            );

            routes.MapRoute(
                name: "Product",
                url: "Shop/{alias}/{subAlias}/{productAlias}",
                defaults: new { controller = "Product", action = "Show" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WebUI.Controllers" }
            );
        }
    }
}

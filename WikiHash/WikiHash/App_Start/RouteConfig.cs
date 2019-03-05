using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WikiHash
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Articles",
                url: "Articles/{link}",
                defaults: new { controller = "Articles", action = "Read" }
            );

            routes.MapRoute(
                name: "Edit",
                url: "Edit/{link}",
                defaults: new { controller = "Edit", action = "Edit" }
            );

            routes.MapRoute(
                name: "Save",
                url: "Save",
                defaults: new { controller = "Edit", action = "Save" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{link}",
                defaults: new { controller = "Home", action = "Index", link = UrlParameter.Optional }
            );
        }
    }
}

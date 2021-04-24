using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pidevShoppyTounsi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(name :"AddRate", url : "{controller}/{action}/{id}/{rate}", defaults: new { controller = "Shelf", action = "AddRate", id = UrlParameter.Optional, rate = UrlParameter.Optional }

);
        }
    }
}

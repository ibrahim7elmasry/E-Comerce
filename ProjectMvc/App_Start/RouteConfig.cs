using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectMvc
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

            routes.MapRoute(
             name: "update",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "SliderImages", action = "Update", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            name: "GetProducts",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "GetproductsOfOneCategory", id = UrlParameter.Optional }
        );


        }
    }
}

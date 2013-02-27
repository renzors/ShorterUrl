using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExamMinEdu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
               name: "Link",
               url: "s/{shortname}",
               defaults: new { controller = "ShortUrl", action = "ShortUrl", shortname = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ShortUrl", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
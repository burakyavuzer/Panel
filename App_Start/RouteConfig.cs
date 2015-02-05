using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yeni_Panel_2015
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Default", url: "{lang}/{id}", defaults: new { controller = "Site", action = "Index",lang = UrlParameter.Optional, id = UrlParameter.Optional });

            routes.MapRoute(name: "DilDegistir_1", url: "Site/DilDegistir/{id}", defaults: new { controller = "Site", action = "SiteDilDegistir", id = UrlParameter.Optional });

        }
    }
}
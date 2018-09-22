using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(

                "SimilarFilters",
                "ApplyGeneralFilter",
                new
                {
                    controller = "GeneralFilters",
                    action = "applyGeneralFilter"
                }
            );

            routes.MapRoute(
                "OneParamsFilter",
                "oneParamFilter",
                new { controller = "GeneralFilters", action = "applyOneParamFilter"}
            );

            routes.MapRoute(
                "ColorChange",
                "ColorsChange",
                new { controller = "GeneralFilters", action = "applyColorChangeFilter" }
            );

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebService
{

    /// <summary>
    /// Clase predetrminada de .net en la que se definen los endpoints y 
    /// se le asocia a cada uno los métodos que les corresponde ejecutar
    /// </summary>
    public class RouteConfig
    {
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            /** Para cada endpoint se especifica
             *  Un nombre (título)
             *  Dirección con la que será accedida
             *  Un controlador que se encargará de atender la petición
             *  Una acción (Se refiere a la función del controlador que ejecutara este endpoint)
            */ 

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
                "OneParamFilter",
                new { controller = "GeneralFilters", action = "applyOneParamFilter"}
            );

            routes.MapRoute(
                "ColorChange",
                "ColorsChange",
                new { controller = "GeneralFilters", action = "applyColorChangeFilter" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

        }
    }
}

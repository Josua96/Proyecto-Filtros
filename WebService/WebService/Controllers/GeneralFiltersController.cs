using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{

    /// <summary>
    /// Contiene tres funciones que se ejecutan para atender las peticiones http, especificamente para realizar fltros sobre imágenes
    /// de tres tipos: Cambio de color, filtros que no requieren parámetros, filtros que requieren un parámetro para funcionar
    /// </summary>
    public class GeneralFiltersController : Controller
    {


        GeneralFilterManager filterManager;

        public GeneralFiltersController()
        {
            filterManager = new GeneralFilterManager();
        }


        /// <summary>
        /// Función que atiende la petición post http
        /// </summary>
        /// <param name="filterInfo"> Clase a la que se asocia los datos que contienen el body de la petición http </param>
        /// <returns> Json con la imagen procesada en formato de string 64 </returns>
        [HttpPost]
        public JsonResult applyGeneralFilter(GeneralFilterInfo filterInfo)
        {

           
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(new { Error= false , id= filterInfo.id, imageData=filterManager.applyFilter
                                                                    (filterInfo.filterName, filterInfo.image)});
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

        /// <summary>
        /// Función que atiende la petición post http
        /// </summary>
        /// <param name="filterInfo">Clase a la que se asocia los datos que contienen el body de la petición http </param>
        /// <returns> Json con la imagen procesada en formato de string 64 </returns>
        [HttpPost]
        public JsonResult applyOneParamFilter(OneParamFilterInfo filterInfo)
        {

            
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(new
                    {
                        Error = false,
                        id = filterInfo.id,
                        imageData = filterManager.applyOneParamFilter(filterInfo.filterName, filterInfo.image, Convert.ToDouble(filterInfo.paramValue.ToString().Replace('.',',')))
                    });
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }


        /// <summary>
        /// Función que atiende la petición post http
        /// </summary>
        /// <param name="filterInfo"> Clase a la que se asocia los datos (requetidos para aplicar el filtro) 
        /// que contienen el body de la petición http </param>
        /// <returns> Json con la imagen procesada en formato de string 64  </returns>
        [HttpPost]
        public JsonResult applyColorChangeFilter(ChangeColorInfo filterInfo)
        {
            
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(new
                    {
                        Error = false,
                        id = filterInfo.id,
                        imageData = filterManager.applyColorSubstitution(filterInfo)
                    });
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

        

    }
}

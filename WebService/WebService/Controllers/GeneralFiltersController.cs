using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class GeneralFiltersController : Controller
    {


        GeneralFilterManager filterManager;

        public GeneralFiltersController()
        {
            filterManager = new GeneralFilterManager();
        }

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
                        imageData = filterManager.applyFilter(filterInfo.filterName, filterInfo.image, filterInfo.paramValue)
                    });
            }
            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

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

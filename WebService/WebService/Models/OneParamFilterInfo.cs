using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{

    /// <summary>
    /// En sus atributos representa los datos que requieren conocer los filtros que utilizan solo
    /// un parámetro para su funcionamiento.
    /// </summary>
    public class OneParamFilterInfo
    {
        public int id { get; set; }
        public string image { get; set; }
        public string filterName { get; set; }

        public string paramValue { get; set; }

    }
}
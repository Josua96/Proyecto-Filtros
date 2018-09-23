using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{

    /// <summary>
    /// En sus atributos representa los datos necesarios para los filtros generales,
    /// los cuales no requieren de ningún parametro para operar
    /// </summary>
    public class GeneralFilterInfo
    {
        public int id { get; set; }
        public string image { get; set; }
        public string filterName { get; set; }
    }
}
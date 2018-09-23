using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebService.Models
{

    /// <summary>
    /// En sus atributos representa los datos requeridos para aplicar el filtro de cambio de color, la imagen se recibe
    /// en formato de un string en base 64
    /// </summary>
    public class ChangeColorInfo
    {
        public int id { get; set; }

        public string image { get; set; }
        
        public string oldColor { get; set; }

        public string newColor { get; set; }

        public string threshold { get; set; }

    }
}
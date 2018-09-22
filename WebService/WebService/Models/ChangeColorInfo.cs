using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ChangeColorInfo
    {
        public int id { get; set; }
        public string image { get; set; }
        
        public Color oldColor { get; set; }

        public Color newColor { get; set; }

        public int threshold { get; set; }

    }
}
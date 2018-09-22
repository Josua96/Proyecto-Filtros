using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros.Clases
{
    class Connections
    {
        string filterName;
        Bitmap bitmap; 

        public Connections()
        {
        }

        public Bitmap applyFilter()
        {
            return bitmap; 
        }
    }
}

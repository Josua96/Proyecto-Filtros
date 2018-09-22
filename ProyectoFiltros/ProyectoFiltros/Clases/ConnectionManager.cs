using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros.Clases
{
    class ConnectionManager
    {
        string filterName;
        Bitmap bitmap; 

        public ConnectionManager()
        {

        }

        public Bitmap applyFilter()
        {
            return bitmap; 
        }
    }
}

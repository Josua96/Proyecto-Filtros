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
        List<Connection> Connections;

        public ConnectionManager()
        {
            Connections = new List<Connection>();
        }


        /// <summary>        
        /// Permite agregar una computadora para que ayude a aplicarle el filtro a un pedaso de imagen        
        /// En este es donde se le asignan los trosos de imagne a cada conexion. 
        /// </summary>
        /// <param name="server"> Es la direccion del web service donde se va a procesar el troso de imagen</param>
        public void AddConnection(String server)
        {            
        }

        /// <summary>
        /// Permite aplicarle el filtro a la imagen previamente dada.        
        /// </summary>
        /// <returns>
        /// Retorna un bitmap de toda la imagen con el filtro aplicado
        /// </returns>
        public Bitmap ApplyFilter()
        {

            return Bitmap;
        }


        /// <summary>
        /// Permite asignar o retornar  el bitmap de la imagen seleccionada. 
        /// </summary>
        public Bitmap Bitmap {
            get => bitmap;
            set => bitmap = value;
        }

        /// <summary>
        /// Asigna o retorna el tipo de filtro que se le va a aplicar.
        /// </summary>
        public string FilterName {
            get => filterName;
            set => filterName = value;
        }

    }
}

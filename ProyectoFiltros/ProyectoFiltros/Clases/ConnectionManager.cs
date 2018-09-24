using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFiltros.Clases
{
    class ConnectionManager
    {
        private string toPath; //Ruta donde se guardan las imagenes procesadas
        string filterName; // Filtro que se quiere aplicar 
        Bitmap bitmap; // Imagen que se le va a aplicar el filtro
        List<Connection> Connections; // Lista de conexiones 
        public List<Color> Colors; // Colores para el filtro de cambio de color 
        private double paramValue; // Valor para los filtros que ocupan parametro especial


        /// <summary>
        /// Es el contructor de la clase
        /// </summary>
        public ConnectionManager()
        {
            Connections = new List<Connection>();
            this.toPath = Directory.GetCurrentDirectory() + "\\outputimages\\" + "image";
            Colors = new List<Color>();
        }
        
        private void SaveImage(Bitmap image)
        {
            image.Save(toPath + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg");
        }

        /// <summary>        
        /// Permite agregar una computadora para que ayude a aplicarle el filtro a un pedaso de imagen        
        /// En este es donde se le asignan los trosos de imagne a cada conexion. 
        /// </summary>
        /// <param name="server"> Es la direccion del web service donde se va a procesar el troso de imagen</param>
        public void AddConnections(List<string> serversList)
        {
            Connections.Clear(); 
            int numberOfServers = serversList.Count; 
            if(bitmap==null)
            {
                return; 
            }
            if(serversList.Count==0)
            {
                return; 
            }
            else
            {
                List<Bitmap> imagesForServers = DivideImage(bitmap, serversList.Count);                 
                for(int i=0; i<serversList.Count; i++)
                {
                    Console.WriteLine(i);
                    if(filterName=="Opacity" || filterName == "Bright" || filterName == "CrudeHighPass")
                    {
                        Console.WriteLine(filterName);
                        Console.WriteLine(filterName);
                        Console.WriteLine(i);
                        Console.WriteLine(imagesForServers.ElementAt(i));
                        Console.WriteLine(ParamValue);
                        Console.WriteLine(serversList.ElementAt(i));                        
                        Connection connection = new Connection(i,imagesForServers.ElementAt(i), filterName,ParamValue, serversList.ElementAt(i));
                        Connections.Add(connection);
                    }
                    else if(filterName=="none")
                    {                        
                        Connection connection = new Connection(i,imagesForServers.ElementAt(i),Colors.ElementAt(0), Colors.ElementAt(1), 60, serversList.ElementAt(i));
                        Connections.Add(connection);
                    }
                    else
                    {                                              
                        Console.WriteLine(i);
                        Console.WriteLine(filterName);
                        Console.WriteLine(imagesForServers.ElementAt(i));                     
                        Console.WriteLine(serversList.ElementAt(i));
                        Connection connection = new Connection(i, filterName, imagesForServers.ElementAt(i), serversList.ElementAt(i));
                        Connections.Add(connection);                        
                    }                 
                }                
            }
        }


        /// <summary>
        /// Divide una imagen en un determinado numero de piezas
        /// </summary>
        /// <param name="image"> Es el bitmap que se va a dividir</param>
        /// <param name="serversNumber">El numero de pedasos</param>
        /// <returns>Una lista de bitmaps que contienen los pedasos de imagen</returns>
        public List<Bitmap> DivideImage(Bitmap image, int serversNumber)
        {
            List<Bitmap> imageSlices = new List<Bitmap>();
            int width = image.Width;
            int height = image.Height;            
            int divisionHeight = height / serversNumber;
            
            for(int i=0; i<serversNumber; i++)
            {
                Bitmap element = new Bitmap(width, divisionHeight);
                for(int y= i*divisionHeight; y< divisionHeight*(i+1); y++)
                {
                    for(int x=0;x< width;x++)
                    {
                        element.SetPixel(x,y-(divisionHeight*i),image.GetPixel(x,y));
                    }
                }                
                imageSlices.Add(element);
            }
            return imageSlices;
        }
    



        /// <summary>
        /// Permite aplicarle el filtro a la imagen previamente dada.        
        /// </summary>
        /// <returns>
        /// Retorna un bitmap de toda la imagen con el filtro aplicado
        /// </returns>
        public Image ApplyFilter()
        {
            try
            {
                Parallel.ForEach(Connections,
             (conextion) =>
             {
                 conextion.ApplyFilterAsync();
             });
            }
            catch(Exception e)
            {
                MessageBox.Show("Error de servidor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
                 
            for(int i=0; i<Connections.Count; i++)
            {
                if(!Connections.ElementAt(i).completed)
                {
                    i = 0; 
                }
            }

            return JoinBitMaps(Connections);             
        }

        /// <summary>
        /// Permite unir imagenes 
        /// </summary>
        /// < name="connectionsList">Recibe la lista de conexiones</param>
        private Bitmap JoinBitMaps(List<Connection> connectionsList)
        {
            int serversNumber = connectionsList.Count;
            if (connectionsList.Count==0)
            {
                //Lanzar error
                return null; 
            }
            else
            {
                Bitmap newImage = new Bitmap(connectionsList.ElementAt(0).Image.Width, connectionsList.ElementAt(0).Image.Height * connectionsList.Count);
                int height = newImage.Height;
                int width = newImage.Width;
                for (int i = 0; i < serversNumber; i++)
                {
                    Bitmap serverImage = connectionsList.ElementAt(i).Image; 
                    for (int y = 0; y< serverImage.Height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            newImage.SetPixel(x, y + (serverImage.Height*i), serverImage.GetPixel(x, y));
                        }                                                                          
                    }                 
                }
                SaveImage(newImage);
                return newImage; 
            }           
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

        /// <summary>
        /// Permite Obtener o Asignar el parametro para ciertos filtros.
        /// </summary>
        public double ParamValue {
            get => paramValue;
            set => paramValue = value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace ProyectoFiltros.Clases
{ 
    class Connection
    {                
        private Bitmap image;
        String connectionAddress;       
        public bool completed;
        private NameValueCollection data; 
        Dictionary<string,string> responseData;

        /// <summary>
        ///   Constructor de la conexion
        /// </summary>
        /// <param name="imagePart">El id del troso de imagen a procesar</param>
        /// <param name="filter">Tipo de filtro a aplicar</param>
        /// <param name="bitmap">El troso de imagen a procesar</param>
        /// <param name="connectionAdds">Es la direcccion del servidor</param>

        public Connection(int imagePart, string filter, Bitmap bitmap, String connectionAdds)
        {                        
            Image = bitmap;
            connectionAddress = connectionAdds+ "/ApplyGeneralFilter";
            completed = false;
            data = new NameValueCollection(); 

            var ms = new MemoryStream();
            Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            string imageB64 = Convert.ToBase64String(ms.GetBuffer());
            data["id"] = imagePart.ToString();
            data["image"] = imageB64;
            data["filterName"] = filter;

            Console.WriteLine(connectionAddress+"     " +filter + "   "+ imagePart.ToString()); 
        }


        /// <summary>
        ///   Constructor de la conexion
        /// </summary>
        /// <param name="id">Idenficador de la conexion</param>
        /// <param name="bitmap">Trozo de imagen a procesar</param>
        /// <param name="filterName">Nombre del filtro</param>
        /// <param name="paramValue">Valor del parametro requerido por el filtro</param>
        /// <param name="connectionAdds">Direccion del servidor</param>
        public Connection(int id, Bitmap bitmap, string filterName,double paramValue, String connectionAdds)
        {
            Image = bitmap;
            connectionAddress = connectionAdds+"/OneParamFilter";
            completed = false;
            data = new NameValueCollection();

            var ms = new MemoryStream();
            Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            string imageB64 = Convert.ToBase64String(ms.GetBuffer());

            data["id"] = id.ToString();
            data["image"] = imageB64;
            data["filterName"] = filterName;
            data["paramValue"] = paramValue.ToString();
        }


        /// <summary>
        ///   Este tipo de conexion solo sirve para el filtro de cambiar color.
        /// </summary>
        /// <param name="id">Idenficador de la conexion</param>
        /// <param name="bitmap">Trozo de la imagen</param>
        /// <param name="oldColor">Color que se quiere cambiar</param>
        /// <param name="newColor">Nueco color</param>
        /// <param name="tolerance">Valor requerido para este filtro</param>
        /// <param name="connectionAdds">Direcccion del servidor</param>
        public Connection(int id, Bitmap bitmap, Color oldColor, Color newColor, double tolerance, String connectionAdds)
        {
            Image = bitmap;
            connectionAddress = connectionAdds+"/ColorsChange";
            completed = false;
            data = new NameValueCollection();

            var ms = new MemoryStream();
            Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            string imageB64 = Convert.ToBase64String(ms.GetBuffer());

            data["id"] = id.ToString();
            data["image"] = imageB64;
            data["oldColor"] =  newColor.A.ToString()+","+newColor.R.ToString() + "," + newColor.G.ToString() + "," + newColor.B.ToString();
            data["newColor"] = oldColor.A.ToString() + "," + oldColor.R.ToString() + "," + oldColor.G.ToString() + "," + oldColor.B.ToString();
            data["threshold"] = tolerance.ToString();
        }
        public Bitmap Image { get => image; set => image = value; }


        /// <summary>
        /// Permite decodificar la respuesta que viene en formato de json pero en string 
        /// </summary>
        /// <param name="response"> Es la respuesta de la consulta</param>
        /// <returns>Un diccionario con los datos de la respuesta del servidor</returns>
        public Dictionary<string, string> DecodingResponse(String response)
        {
            response = response.Remove(0, 1);
            response = response.Remove(response.Length - 1, 1);            
            Dictionary<string,string> dictionary = new Dictionary<string, string>();
            String[] data = response.Split(',');             
            foreach(string keyValue in data)
            {                
                string[] keyValueList = keyValue.Split(':');                
                dictionary.Add(keyValueList[0].Trim('"'), keyValueList[1].Trim('"')); 
            }           
            return dictionary; 
        }

        /// <summary>
        /// Permite decodificar una imagen que esta como formato string
        /// </summary>
        /// <param name="base64Image">Es la imagen en formato string</param>
        /// <returns>Bitmap de una imagen</returns>
        public Bitmap DecodeImageFromB64(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);            
            ms.Write(imageBytes, 0, imageBytes.Length);
            Bitmap imageConverted  = new Bitmap(System.Drawing.Image.FromStream(ms, true));
            return imageConverted;
        }


        /// <summary>
        ///  Permite llamar al web service para aplicar el filtro 
        /// </summary>                
        public void ApplyFilterAsync()
        {                                
            using (var wb = new WebClient())
            {                                     
                var response =  wb.UploadValues(connectionAddress, "POST", data);                
                try
                {
                    responseData = DecodingResponse(Encoding.UTF8.GetString(response));
                    Image = DecodeImageFromB64(responseData["imageData"]);
                    completed = true; 
                }
                catch(Exception e)
                {
                }                         
            }
        }
        
   }
}

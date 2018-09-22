using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros.Clases
{

    class Connection
    {
        private static readonly HttpClient client = new HttpClient();
        string filtername;
        Bitmap image;
        String connectionAddress;
        int id; 
        public Connection(int imagePart, string filter, Bitmap bitmap, String connectionAdds)
        {
            id = imagePart;
            filtername = filter;
            image = bitmap;
            connectionAddress = connectionAdds; 
        }
        /// <summary>
        ///     Permite llamar al web service para aplicar el filtro 
        /// </summary>
        /// <returns>Rtorna la imagen procesada</returns>
        public async Task<Bitmap> applyFilterAsync()
        {            
            var values = new Dictionary<string, string>
            {
               { "id", "hello" },
               { "thing2", "world" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return image; 
        }
   }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WebService.Models
{

    /// <summary>
    /// Convierte de un string en base64 a imagen
    /// Converite una imagen a string en base 64
    /// </summary>
    public class ImageManager
    {

        public ImageManager()
        {

        }

        /// <summary>
        /// Convierte una imagen a string en base 64
        /// </summary>
        /// <param name="image"> Mapa de bits (pixeles) de la imagen a convertir</param>
        /// <returns> Imagen representada como un string en base64</returns>
        public string encodeImage(Bitmap image)
        {

            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap(image))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                }
            }
          
        } 

        /// <summary>
        /// Convierte un string en base64 a una imagen
        /// </summary>
        /// <param name="base64Image"></param>
        /// <returns> La imagen que representa el string en base 64</returns>
        public Image decodeImage(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ImageManager
    {

        public ImageManager()
        {

        }

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
            /*
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms,ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }

            */


            /*
             
            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap(bitMapImage))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                }
            }

            */

            /*

            byte[] imageBytes = Convert.FromBase64String(image);
            //Image image;

            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                image = Image.FromStream(ms, true);

                Bitmap result = applyFilterAux(imageToFilter, image);//
                return encodeBase64String(result);
            }

            */

        } 

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
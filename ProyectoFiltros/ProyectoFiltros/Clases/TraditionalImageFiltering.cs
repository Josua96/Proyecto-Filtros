using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFiltros
{
    class TraditionalImageFiltering
    {

        private pixelTransformer pixelManager;

        private string toPath;

        public TraditionalImageFiltering()
        {
            this.pixelManager = new pixelTransformer();
            this.toPath = Directory.GetCurrentDirectory() + "\\outputimages\\" + "image";
        }

        private void saveImage (Bitmap image)
        {
            image.Save(toPath + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".jpg");
        }

        public void sepiaFilter (Bitmap image)
        {
            int width = image.Width;
            int heigth = image.Height;
            Color pixel;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, pixelManager.toSepia(pixel));
                }

            }

            saveImage(image);

        }

        public void grayScaleFilter (Bitmap image)
        {
            int width = image.Width;
            int heigth = image.Height;



            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    Color pixel = image.GetPixel(x, y);
                    image.SetPixel(x, y, pixelManager.toGrayScale(pixel));
                }

            }

            saveImage(image);

        }


    }
}
